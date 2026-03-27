document.addEventListener("DOMContentLoaded", () => {
    const cartItemsWrapper = document.getElementById("cart-items-wrapper");
    if (!cartItemsWrapper) return; 

    let cartData = [
        {
            id: 1,
            name: "Organic Vine Tomatoes",
            category: "Vegetables",
            price: 4.99,
            quantity: 3,
            unit: "per kg",
            image: "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?auto=format&fit=crop&q=80&w=200"
        }
    ];

    const shippingCostDefault = 4.99;
    const freeShippingThreshold = 40.00;

    const itemCountText = document.getElementById("cart-item-count");
    const subtotalText = document.getElementById("summary-subtotal-text");
    const subtotalPrice = document.getElementById("summary-subtotal-price");
    const shippingPrice = document.getElementById("summary-shipping-price");
    const totalPrice = document.getElementById("summary-total-price");
    const freeShippingAlert = document.getElementById("free-shipping-alert");
    const clearCartBtn = document.getElementById("clear-cart-btn");

    function renderCart() {
        cartItemsWrapper.innerHTML = ""; 

        if (cartData.length === 0) {
            cartItemsWrapper.innerHTML = `<div class="empty-cart-msg">Your cart is empty. <br><br> <a href="market.html" style="color:var(--primary-green); text-decoration:underline;">Go to Market</a></div>`;
            clearCartBtn.style.display = 'none';
        } else {
            clearCartBtn.style.display = 'block';
            cartData.forEach(item => {
                const itemTotal = (item.price * item.quantity).toFixed(2);
                const itemHTML = `
                    <div class="cart-item-row" data-id="${item.id}">
                        <img src="${item.image}" alt="${item.name}" class="cart-item-img">
                        <div class="cart-item-details">
                            <h3 class="cart-item-title">${item.name}</h3>
                            <p class="cart-item-cat">${item.category}</p>
                            <div class="cart-item-price">$${item.price} <span>${item.unit}</span></div>
                            <button class="delete-item-btn" onclick="removeItem(${item.id})"><i class="fa-solid fa-trash-can"></i></button>
                        </div>
                        <div class="cart-item-actions">
                            <div class="qty-control">
                                <button class="qty-btn" onclick="updateQty(${item.id}, -1)"><i class="fa-solid fa-minus"></i></button>
                                <span class="qty-value">${item.quantity}</span>
                                <button class="qty-btn" onclick="updateQty(${item.id}, 1)"><i class="fa-solid fa-plus"></i></button>
                            </div>
                            <div class="item-total">$${itemTotal}</div>
                        </div>
                    </div>
                `;
                cartItemsWrapper.insertAdjacentHTML('beforeend', itemHTML);
            });
        }
        updateSummary();
    }

    function updateSummary() {
        let totalItems = 0;
        let subtotal = 0;

        cartData.forEach(item => {
            totalItems += item.quantity;
            subtotal += (item.price * item.quantity);
        });

        itemCountText.textContent = `${totalItems} items ready to harvest`;
        subtotalText.textContent = `Subtotal (${totalItems} items)`;
        subtotalPrice.textContent = `$${subtotal.toFixed(2)}`;

        let shipping = 0;
        if (totalItems > 0) {
            if (subtotal >= freeShippingThreshold) {
                shipping = 0;
                shippingPrice.textContent = "Free";
                freeShippingAlert.textContent = "You've unlocked free shipping !";
                freeShippingAlert.className = "free-shipping-alert free-shipping-success";
            } else {
                shipping = shippingCostDefault;
                shippingPrice.textContent = `$${shipping.toFixed(2)}`;
                const diff = (freeShippingThreshold - subtotal).toFixed(2);
                freeShippingAlert.textContent = `Add $${diff} more for free shipping`;
                freeShippingAlert.className = "free-shipping-alert";
            }
        } else {
            shippingPrice.textContent = "$0.00";
            freeShippingAlert.textContent = "Add items to your cart";
            freeShippingAlert.className = "free-shipping-alert";
        }

        const total = subtotal + shipping;
        totalPrice.textContent = `$${total.toFixed(2)}`;
    }

    window.updateQty = function(id, change) {
        const item = cartData.find(i => i.id === id);
        if (item) {
            item.quantity += change;
            if (item.quantity < 1) item.quantity = 1;
            renderCart();
        }
    };

    window.removeItem = function(id) {
        cartData = cartData.filter(i => i.id !== id);
        renderCart();
    };

    if (clearCartBtn) {
        clearCartBtn.addEventListener("click", () => {
            cartData = [];
            renderCart();
        });
    }
    renderCart();
});