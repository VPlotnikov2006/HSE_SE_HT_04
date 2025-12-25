const USERS = [
  { id: 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', name: 'Alice' },
  { id: 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', name: 'Bob' },
  { id: 'cccccccc-cccc-cccc-cccc-cccccccccccc', name: 'Carol' }
];

const PRODUCTS = [
  { id: '11111111-1111-1111-1111-111111111111', name: 'Christmas Sweater', price: 2999.00 },
  { id: '22222222-2222-2222-2222-222222222222', name: 'Wireless Headphones', price: 8999.00 },
  { id: '33333333-3333-3333-3333-333333333333', name: 'Premium Coffee Beans', price: 1499.00 },
  { id: '44444444-4444-4444-4444-444444444444', name: 'Vitamin C Serum', price: 2499.00 },
  { id: '55555555-5555-5555-5555-555555555555', name: 'Smart Fitness Watch', price: 12999.00 }
];

const logEl = document.getElementById('log');
const userSelect = document.getElementById('userSelect');
const balanceDisplay = document.getElementById('balanceDisplay');
const ordersDisplay = document.getElementById('ordersDisplay');

function log(...args){
  const line = args.map(a=>typeof a==='object'?JSON.stringify(a,null,2):String(a)).join(' ');
  logEl.textContent += line + '\n';
  logEl.scrollTop = logEl.scrollHeight;
}

function $(id){return document.getElementById(id)}

function api(path, opts){
  return fetch('/api/' + path, opts).then(async r => {
    const text = await r.text();
    try{const json = JSON.parse(text); return { ok: r.ok, status: r.status, json } }catch{ return { ok: r.ok, status: r.status, text } }
  });
}

function init(){
  USERS.forEach(u => {
    const opt = document.createElement('option'); opt.value = u.id; opt.textContent = `${u.name} (${u.id})`; userSelect.appendChild(opt);
  });

  const productsDiv = document.getElementById('products');
  PRODUCTS.forEach(p=>{
    const el = document.createElement('div'); el.className='product';
    el.innerHTML = `<label>${p.name} — ${p.price.toFixed(2)}</label>`;
    const qty = document.createElement('input'); qty.type='number'; qty.min='0'; qty.value='0'; qty.className='qty';
    qty.dataset.pid = p.id;
    el.appendChild(qty);
    productsDiv.appendChild(el);
  });

  $('createAccountBtn').addEventListener('click', createAccount);
  $('getBalanceBtn').addEventListener('click', getBalance);
  $('depositBtn').addEventListener('click', deposit);
  $('listOrdersBtn').addEventListener('click', listOrders);
  $('getOrderBtn').addEventListener('click', getOrderById);
  $('createOrderBtn').addEventListener('click', createOrder);
}

async function createAccount(){
  const userId = userSelect.value;
  log('Create account for', userId);
  const res = await api('ps/create-account', {
    method: 'POST', headers:{'Content-Type':'application/json'},
    body: JSON.stringify({ userId })
  });
  log('response', res);
}

async function getBalance(){
  const userId = userSelect.value;
  log('Get balance for', userId);
  const res = await api(`ps/balance/${userId}`);
  if(res.ok){
    const data = res.json;
    balanceDisplay.textContent = `Balance: ${data.balance.toFixed(2)} (user ${data.userId})`;
    log('balance', data);
  } else {
    balanceDisplay.textContent = 'Error: ' + (res.status || JSON.stringify(res.text));
    log('error', res);
  }
}

async function deposit(){
  const userId = userSelect.value;
  const amount = parseFloat($('depositAmount').value || 0);
  if(isNaN(amount) || amount <= 0){ alert('Amount must be positive'); return; }
  log('Deposit', amount, 'to', userId);
  const res = await api('ps/deposit', { method: 'PUT', headers:{'Content-Type':'application/json'}, body: JSON.stringify({ userId, balanceDelta: amount }) });
  log('response', res);
  if(res.ok) getBalance();
}

async function listOrders(){
  log('Listing orders');
  const res = await api('os/orders');
  if(res.ok){ log('orders', res.json); }
  else { log('error', res); }
}

async function getOrderById(){
  const id = $('orderIdInput').value.trim(); if(!id){ alert('Enter order id'); return; }
  log('Get order', id);
  const res = await api(`os/orders/${id}`);
  if(res.ok){ log('order', res.json); }
  else { log('error', res); }
}

async function createOrder(){
  const userId = userSelect.value;
  const items = Array.from(document.querySelectorAll('#products .product input'))
    .map(i=>({ productId: i.dataset.pid, quantity: Math.max(0, Math.floor(Number(i.value) || 0)) }))
    .filter(x=>x.quantity>0);
  if(items.length===0){ alert('Choose at least one product with quantity>0'); return; }
  log('Create order', { userId, items });
  const res = await api('os/new-order', { method: 'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify({ userId, items }) });
  if(res.ok){ const created = res.json; log('created', created); alert('Order created: ' + (created.orderId || JSON.stringify(created))); }
  else { log('error', res); alert('Failed to create order'); }
}

window.addEventListener('load', init);
