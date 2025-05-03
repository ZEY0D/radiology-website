const items = document.querySelectorAll('#sidebarItems li');

items.forEach((item, index) => {
  item.addEventListener('click', () => {
    items.forEach(el => el.classList.remove('active'));
    item.classList.add('active');
  });
});