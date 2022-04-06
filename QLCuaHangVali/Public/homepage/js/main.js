const $ = document.querySelector.bind(document); // Lấy 1 element
const $$ = document.querySelectorAll.bind(document); // Lấy nhiều elementvar
//Code cho droplist user 
const info_username = $('.info_username');
const UserName_drop = $('.UserName_drop');
const user_icon = $('.dropdown-item_user_icon span');


info_username.onclick = function () {
    UserName_drop.classList.toggle('active');
}

// Đóng Profile của khách hàng 
user_icon.onclick = function () {
    UserName_drop.classList.toggle('active');
}


// Code phần đăng nhập
const name_user = $('.name_user')
const login = $('.login')
const dangnhap = $(".dangnhap")
const info1 = $('.check_login1')
const btn_close = $('.btn_close_login');

// Nếu đăng nhậ thành công thì xóa thông báo để khỏi hiện trang login bến dưới
if (name_user.textContent != "") {
    info1.innerText = "";
}

// Nếu đăng nhập sai thì hiện lại tran login
if (info1.textContent == "Tên đăng nhập hoặc mật khẩu không đúng") {
    login.click();
}


// Nếu đóng thì xóa thông báo 
btn_close.onclick = function () {
    info1.innerText = "";
}

function reloadVali() {
    //window.location.reload();
}
