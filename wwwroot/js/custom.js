// to get current year
function getYear() {
    var currentDate = new Date();
    var currentYear = currentDate.getFullYear();
    document.querySelector("#displayYear").innerHTML = currentYear;
}


//  owl carousel script
$(".owl-carousel").owlCarousel({
    loop: true,
    margin: 20,
    nav: true,
    autoplay: true,
    navText: ['<i class="fa fa-long-arrow-left" aria-hidden="true"></i>', '<i class="fa fa-long-arrow-right" aria-hidden="true"></i>'],
    autoplayHoverPause: true,
    responsive: {
        0: {
            items: 1
        },
        991: {
            items: 2
        }
    }
});

//    end owl carousel script 

function getRequest(url) {
    return $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        error: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus + ":" + errorThrown);
        }
    });
}

function postRequest(url, data) {
    if ((typeof data) == "object") {
        data = JSON.stringify(data)
    }
    return $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        data: data,
        error: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus + ": " + errorThrown);
        }
    });
}

function postFileRequest(url, data) {
    return $.ajax({
        url: url,
        type: "POST",
        enctype: 'multipart/form-data',
        data: data,
        processData: false,
        contentType: false,
        error: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus + ": " + errorThrown);
        }
    });
}