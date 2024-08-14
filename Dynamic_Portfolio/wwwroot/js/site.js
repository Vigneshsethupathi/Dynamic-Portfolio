

var user = document.getElementById("userError");
var password = document.getElementById("passwordError");
var login_button = document.getElementById("loginButton");

login_button.addEventListener('click', function () {
    user.classList.add("user_valid");
    password.classList.add("pass_valid");
});


