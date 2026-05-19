const pdMinusBtn = document.getElementById("pd-minus");
const pdPlusBtn = document.getElementById("pd-plus");
const pdQtyValue = document.getElementById("pd-qty");
const pdWishlistBtn = document.getElementById("pd-wishlist");
const productAddButtons = document.querySelectorAll(".btn-add-to-cart");
if (pdMinusBtn && pdPlusBtn && pdQtyValue) {
    let currentQty = parseInt(pdQtyValue.textContent);

    pdPlusBtn.addEventListener("click", () => {
        currentQty++;
        pdQtyValue.textContent = currentQty;
    });

    pdMinusBtn.addEventListener("click", () => {
        if (currentQty > 1) {
            currentQty--;
            pdQtyValue.textContent = currentQty;
        }
    });
}
let Quantity = document.querySelector(".qty-value");
productAddButtons.forEach((btn) => {
    btn.addEventListener("click", function () {
        cartBadge.innerHTML = `${+cartBadge.innerHTML + +Quantity.innerHTML}`;
        cartBadge.classList.add("bump");
        setTimeout(() => {
            cartBadge.classList.remove("bump");
        }, 200);
    });
});

$(document).ready(function () {

    let timeout = null;

    let currentCategory = "All";

    function loadProducts() {

        $.ajax({

            url: '/Product/Search',

            type: 'GET',

            data: {

                searchTerm:
                    $("#searchInput").val(),

                categoryName:
                    currentCategory,

                sortOrder:
                    $("#sortSelect").val()
            },

            beforeSend: function () {

                $("#productsContainer").html(`

                    <div class="loading-box">

                        <div class="spinner-border text-success"
                             role="status">
                        </div>

                    </div>

                `);

            },

            success: function (result) {

                $("#productsContainer")
                    .html(result);

                updateProductsCount();

            },

            error: function () {

                $("#productsContainer").html(`

                    <div class="text-danger text-center">

                        Failed To Load Products

                    </div>

                `);

            }

        });

    }

    function updateProductsCount() {

        let count =
            $("#productsContainer .product-card")
                .length;

        $(".results-count strong")
            .text(count);

    }


    $("#searchInput").on("keyup", function () {

        clearTimeout(timeout);

        timeout = setTimeout(function () {

            loadProducts();

        }, 400);

    });

    $("#sortSelect").on("change", function () {

        loadProducts();

    });

    $(document).on("click", ".pill-btn", function () {

        $(".pill-btn")
            .removeClass("active");

        $(this)
            .addClass("active");

        currentCategory =
            $(this).data("category");
        console.log(currentCategory);
        loadProducts();

    });

});