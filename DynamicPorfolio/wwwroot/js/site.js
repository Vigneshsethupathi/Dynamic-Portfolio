      //--------- this code was  login email---------------------->
var modal = document.getElementById('myModal');
var btn = document.querySelector('.register_email');
var span = document.getElementsByClassName('close')[0];

btn.onclick = function () {
    modal.style.display = "block";
    document.body.classList.add('modal-open');
}
span.onclick = function () {
    modal.style.display = "none";
    document.body.classList.remove('modal-open');
}
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
        document.body.classList.remove('modal-open');
    }
}

// ---------------- this code was register email---------------------->

var create_email = document.getElementById("create-email");
var register_page = document.getElementById("Register_page");
var modal = document.getElementById("myModal");
var register_page_close = document.getElementById("register_page_close");

create_email.onclick = function () {
    modal.style.display = "none";
    document.body.classList.remove('modal-open');
    register_page.style.display = "revert";
}
register_page_close.onclick = function () {
    modal.style.display = "revert";
    document.body.classList.add('modal-open');
    register_page.style.display = "none";
}



//--------------this code is index page animation-------------->

var modal = document.getElementById("myModal");
var btn = document.getElementById("Register_BTN");
var span = document.getElementsByClassName("close")[0];

// Ensure that the elements are not null before attaching event listeners
if (modal && btn && span) {
    // Button click opens the modal
    btn.onclick = function () {
        modal.style.display = "block";
        document.body.classList.add('modal-open');
    };

    // Close button click closes the modal
    span.onclick = function () {
        modal.style.display = "none";
        document.body.classList.remove('modal-open');
    };

    // Clicking outside the modal closes it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
            document.body.classList.remove('modal-open');
        }
    };
} else {
    console.error("Modal, button, or close element is missing.");
}

// In this code was Me >  button animation -------------------->


