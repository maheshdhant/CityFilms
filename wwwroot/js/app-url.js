var apiBaseUrl = "/api/";
var htmlBaseUrl = "/html/";
var htmlUrl = {
    partners: htmlBaseUrl + "partner.html",
    about: htmlBaseUrl + "about.html",
    contact: htmlBaseUrl + "contact.html",
    service: htmlBaseUrl + "service.html",
    booking: htmlBaseUrl + "booknow.html"
}
var htmlPublicBaseUrl = htmlBaseUrl + "public/";
var htmlPublicUrl = {
    
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
    selectBackground: apiControlAdminBaseUrl + "select-bg-image/",
    deleteBackground: apiControlAdminBaseUrl + "delete-bg-image/",
    addPartnerInfo: apiControlAdminBaseUrl + "add-partner-info/",
}

var apiControlHomeBaseUrl = apiBaseUrl + "control/home/"
var apiControlHomeUrl = {
    currentBackground: apiControlHomeBaseUrl + "current-bg",
    logo: apiControlHomeBaseUrl + "company-logo",
    partnerInfo: apiControlHomeBaseUrl + "partner-info",
}


// hashValue
var hashValue = {
    home: "#index",
    partners: "#partners",
    about: "#about",
    service: "#service",
    contact: "#contact",
    booking: "#booking",
    adminDashboard: "#admin/dashboard",
}