document.querySelector('.en-menu-toggle')?.addEventListener('click', (event) => {
  const nav = document.querySelector('.en-nav');
  const open = nav?.classList.toggle('open');
  event.currentTarget.setAttribute('aria-expanded', String(Boolean(open)));
});

document.querySelector('#demo-search')?.addEventListener('submit', (event) => {
  event.preventDefault();
  const form = event.currentTarget;
  const terms = ['query', 'province', 'city'].map(name =>
    String(form.elements.namedItem(name)?.value || '').trim().toLowerCase()
  );
  let visible = 0;
  document.querySelectorAll('.en-property-card').forEach(card => {
    const content = card.textContent.toLowerCase();
    const matched = terms.every(term => !term || content.includes(term));
    card.hidden = !matched;
    if (matched) visible++;
  });
  let empty = document.querySelector('.en-empty');
  if (!empty) {
    empty = document.createElement('div');
    empty.className = 'en-empty';
    empty.textContent = 'No homes match this search. Try another city or keyword.';
    document.querySelector('.en-property-grid')?.append(empty);
  }
  empty.hidden = visible > 0;
  document.querySelector('#homes')?.scrollIntoView({ behavior: 'smooth' });
});
