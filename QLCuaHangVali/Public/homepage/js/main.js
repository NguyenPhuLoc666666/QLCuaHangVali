
    // Xử lý phần Profile hồ sơ của khách hàng 
    jQuery(document).ready(function ($) {
        $(".info_username").click(function (event) {
            $(".UserName_drop").addClass('active');
        });
    });

    jQuery(document).ready(function ($) {
        $(".dropdown-item_user_icon span").click(function (event) {
            $(".UserName_drop").removeClass('active');
        });
    });

    // Xử lý phần đăng nhập
    jQuery(document).ready(function ($) {
        $(".login").click(function (event) {
          // Xử lý phần tự động click
        });
    });
