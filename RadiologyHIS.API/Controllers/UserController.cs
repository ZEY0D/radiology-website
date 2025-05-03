using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadiologyHIS.API.Data.Service;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _iUserService;
        public UserController(IUserService userService){
            _iUserService = userService;
        }


        // GET /api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(){
            var users = await _iUserService.GetAllUsersAsync();
            return Ok(users);   // data returned in a json format 
        }

        // GET /api/users/id	
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id){
            var user = await _iUserService.GetUserByIdAsync(id);
            if (user == null){
                return NotFound();
            }
            return Ok(user);
        }


        // POST /api/users	
        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] User user){
            var newUser = await _iUserService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserByIdAsync), new { id = newUser.Id }, newUser);

        }


        // PUT /api/users/id	
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] User user){
            var updatedUser = await _iUserService.UpdateUserAsync(id, user);
            if (updatedUser == null){
                return NotFound();
            }
            return Ok(updatedUser);
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _iUserService.DeleteUserAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }



        [HttpPost("{id}/upload-profile-image")]
public async Task<IActionResult> UploadProfileImage(int id, IFormFile file)
{
    if (file == null || file.Length == 0)
        return BadRequest("No file uploaded.");

    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
    var extension = Path.GetExtension(file.FileName).ToLower();

    if (!allowedExtensions.Contains(extension))
        return BadRequest("Only .jpg, .jpeg, and .png files are allowed.");

    if (file.Length > 8 * 1024 * 1024) // 2 MB max
        return BadRequest("File size must be less than 8 MB.");

    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile-images");
    if (!Directory.Exists(uploadsFolder))
        Directory.CreateDirectory(uploadsFolder);

    var user = await _iUserService.GetUserByIdAsync(id);
    if (user == null)
        return NotFound();

    // Delete old image if exists
    if (!string.IsNullOrEmpty(user.ProfileImage))
    {
        var oldImagePath = Path.Combine(uploadsFolder, user.ProfileImage);
        if (System.IO.File.Exists(oldImagePath))
            System.IO.File.Delete(oldImagePath);
    }

    var uniqueFileName = Guid.NewGuid().ToString() + extension;
    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    user.ProfileImage = uniqueFileName;
    await _iUserService.UpdateUserAsync(id, user);

    return Ok(new { message = "Profile image uploaded successfully.", fileName = uniqueFileName });
}




        [HttpGet("{id}/profile-image-url")]
        public async Task<IActionResult> GetProfileImageUrl(int id)
        {
            var user = await _iUserService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User not found.");

            // Fallback image
            var fileName = string.IsNullOrEmpty(user.ProfileImage) ? "default.png" : user.ProfileImage;
            var imageUrl = $"{Request.Scheme}://{Request.Host}/profile-images/{fileName}";
            
            return Ok(new { imageUrl });
        }




        [HttpDelete("{id}/profile-image")]
        public async Task<IActionResult> DeleteProfileImage(int id)
        {
            var user = await _iUserService.GetUserByIdAsync(id);
            if (user == null || string.IsNullOrEmpty(user.ProfileImage))
                return NotFound("Profile image not found.");

            // Full path to image
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profile", user.ProfileImage);

            // Delete file if exists
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            // Clear the database field
            user.ProfileImage = null;
            await _iUserService.UpdateUserAsync(id, user);

            return NoContent();
        }

    }
}
