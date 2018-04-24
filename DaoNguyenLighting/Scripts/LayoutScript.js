function Scroll_Y_Disable() {
    var y = document.getElementById("product-body");
    var x = document.getElementById("body");
    if (y.style.display === "block") {
        x.style.overflowY = "hidden";
        y.style.overflowY = "initial";
    }
}

function Auto_Edit_Padding_Product() {
    var w = window.innerWidth;
    var x = document.getElementById("product-cover");
    var y = document.getElementById("product-content");
    var z = document.getElementById("product-body");
    var s = screen.width;

    if (w / s >= 0.8)
        {          
            x.style.paddingTop = "20px";
            x.style.paddingLeft = "120px";
            x.style.paddingRight = "120px";
            $(".pr0duct-c0v3r").css({ "max-width": w - 240 + "px" });
        }
        else if (w/s >= 0.6) {
            x.style.paddingTop = "20px";
            x.style.paddingLeft = "70px";
            x.style.paddingRight = "70px";
            $(".pr0duct-c0v3r").css({ "max-width": w - 140 + "px" });
        }
        else if (w/s >= 0.4) {
            x.style.paddingTop = "60px";
            x.style.paddingLeft = "12px";
            x.style.paddingRight = "12px";
            $(".pr0duct-c0v3r").css({ "max-width": w - 24 + "px" });
        }
    var new_height_img = parseInt($(".pr0duct-1mg-lvl2 img").css("height"));
    $(".pr0duct-c0nt3nt-wr4p").css({ "height": new_height_img + 142 + "px" });
    $(".pr0duct-c0nt3nt").css({ "height": new_height_img + 142 + "px" });
}
function Auto_Edit_Margin_Product_Detail() {
    var original_height_img = parseInt($(".pr0duct-d3t41l-1m4g3-lvl1 img").css("height"));
    var original_height_div = parseInt($(".pr0duct-d3t41l-1m4g3-lvl1").css("height"));
    if (original_height_img < original_height_div) {
        $(".pr0duct-d3t41l-1m4g3-lvl1 img").css({ "margin-top": (458 - original_height_img) / 2 })
    }
   
}

function ChangeUrl(page, url) {
    if (typeof (history.pushState) != "undefined") {
        var obj = { Page: page, Url: url };
        history.pushState(obj, obj.Page, obj.Url);
    } else {
        alert("Browser does not support HTML5.");
    }
}

function Show_Product_Detail() {
   
    $(".pr0duct-d3t41l-1m4g3-lvl1").on("click", (function (event) {
       
        var idImg = event.target.id;
        var img = document.getElementById(idImg);
        var src_img = img.getAttribute("src");

        var z = document.getElementById("product-body");

        var width = img.clientWidth;
        var height = img.clientHeight;
        
       
        z.style.display = "block";
        Scroll_Y_Disable();

        if (width >= height) {
            $(".pr0duct-1mg-lvl2").css({ "min-width": "200px", "max-width": "500px" });
            $(".pr0duct-1mg-lvl2").prepend("<img src='" + src_img + "' id='PIID' />");
            Auto_Resize_Product();
        }
        else {
            $(".pr0duct-1mg-lvl2").css({ "min-width": "100px", "max-width": "calc((100vh) * 0.5)" });
            $(".pr0duct-1mg-lvl2").prepend("<img src='" + src_img + "' id='PIID'/>"); 
            Auto_Resize_Product();
        }
    }));
}


//HIDE PRODUCT'S BUTTON
function Hide_Product() {
    var y = document.getElementById("product-body");
    var x = document.getElementById("body");
    if (y.style.display === "none" && $('#PIID').length > 0) {
        y.style.display = "block";
    } else {
        y.style.display = "none";
        x.style.overflowY = "initial";
        $(".pr0duct-1mg-lvl2 img:last-child").remove();
        location.reload();
    }

}

function Auto_Resize_Product() {
    var w = window.innerWidth;
    var original_height_div = parseInt($(".pr0duct-c0nt3nt-b0dy").css("height")) - 24;
    var original_height_img = parseInt($(".pr0duct-1mg-lvl2 img").css("height"));
    var original_width_img = parseInt($(".pr0duct-1mg-lvl2 img").css("width"));
    var maxwidth = original_width_img * (original_height_div / original_height_img);
    $(".pr0duct-1mg-lvl2").css({ "max-width": maxwidth });
    var new_height_img = parseInt($(".pr0duct-1mg-lvl2 img").css("height"));
    $(".pr0duct-c0nt3nt").css({ "height": new_height_img + 142 + "px" });
    $(".pr0duct-c0nt3nt-wr4p").css({ "height": new_height_img + 142 + "px" });
}

function Zoom_Image() {
    $(".pr0duct-1mg-lvl2").on("click", (function (event) {
        var wrap_width_div = parseInt($(".pr0duct-c0nt3nt-wr4p").css("width"));
        var original_width_img = parseInt($(".pr0duct-1mg-lvl2 img").css("width"));
        if (original_width_img < wrap_width_div - 32) {
            $(".pr0duct-1mg-lvl2").css({ "cursor": "zoom-out","min-width": "100%" });
            var zoom_in_height_img = parseInt($(".pr0duct-1mg-lvl2 img").css("height"));
            $(".pr0duct-c0nt3nt").css({ "height": zoom_in_height_img + 142 + "px" });
            $(".pr0duct-c0nt3nt-wr4p").css({ "height": zoom_in_height_img + 142 + "px" });
        }
        else {
            $(".pr0duct-1mg-lvl2").css({ "cursor": "zoom-in", "min-width": "100px" });
            var zoom_out_height_img = parseInt($(".pr0duct-1mg-lvl2 img").css("height"));
            $(".pr0duct-c0nt3nt").css({ "height": zoom_out_height_img + 142 + "px" });
            $(".pr0duct-c0nt3nt-wr4p").css({ "height": zoom_out_height_img + 142 + "px" });
        }
     
    }));
}
