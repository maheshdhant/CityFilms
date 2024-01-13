var apiBaseUrl = "/api/";
var htmlBaseUrl = "/html/";
var htmlUrl = {
    partners: htmlBaseUrl + "partner.html",
    about: htmlBaseUrl + "about.html",
    contact: htmlBaseUrl + "contact.html",
    sercices: htmlBaseUrl + "sercice.html",
    booking: htmlBaseUrl + "booknow.html"
}
var htmlControlAdminBaseUrl = htmlBaseUrl + "control/admin/";
var htmlControlAdminUrl = {
    adminDashboard: htmlControlAdminBaseUrl + "admindashboard.html"
}
var apiControlAdminBaseUrl = apiBaseUrl + "control/admin/"
var apiControlAdminUrl = {
    imageList: apiControlAdminBaseUrl + "images-list",
    backgroundImageList: apiControlAdminBaseUrl + "backgroundImages-list",
    uploadImage: apiControlAdminBaseUrl + "upload-images",
    selectBackground: apiControlAdminBaseUrl + "select-bg-image",
}
var apiControlHomeBaseUrl = apiBaseUrl + "control/home/"
var apiControlHomeUrl = {
    currentBackground: apiControlHomeBaseUrl + "current-bg",
}


// hashValue
var hashValue = {
    home: "#index",
    partners: "#partners",
    about: "#about",
    services: "#services",
    contact: "#contact",
    booking: "#booking",
    adminDashboard: "#admin/dashboard",
}