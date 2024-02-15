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
var htmlAuthBaseUrl = htmlBaseUrl + "Auth/";
var htmlAuthUrl = {
    login : htmlAuthBaseUrl + "login.html",

}
var htmlControlAdminBaseUrl = htmlBaseUrl + "control/admin/";
var htmlControlAdminUrl = {
    adminDashboard: htmlControlAdminBaseUrl + "admindashboard.html"
}

var apiAuthBaseUrl = apiBaseUrl + "auth/";
var apiAuthUrl = {
    registerUser : apiAuthBaseUrl + "register",
    login : apiAuthBaseUrl + "login"
}
var apiControlAdminBaseUrl = apiBaseUrl + "control/admin/"
var apiControlAdminUrl = {
    imageList: apiControlAdminBaseUrl + "images-list",
    backgroundImageList: apiControlAdminBaseUrl + "backgroundImages-list",
    uploadImage: apiControlAdminBaseUrl + "upload-images",
    selectBackground: apiControlAdminBaseUrl + "select-bg-image/",
    deleteBackground: apiControlAdminBaseUrl + "delete-bg-image/",
    addPartnerInfo: apiControlAdminBaseUrl + "add-partner-info/",
    getPartnerInfo: apiControlAdminBaseUrl + "get-partner-info/",
    editPartnerInfo: apiControlAdminBaseUrl + "edit-partner-info/",
    getCompanyProfile: apiControlAdminBaseUrl + "get-company-profile/",
    updateCompanyProfile: apiControlAdminBaseUrl + "edit-company-profile/",
}

var apiControlHomeBaseUrl = apiBaseUrl + "control/home/"
var apiControlHomeUrl = {
    currentBackground: apiControlHomeBaseUrl + "current-bg",
    logo: apiControlHomeBaseUrl + "company-logo",
    partnerInfo: apiControlHomeBaseUrl + "partner-info",
    sendEmail: apiControlHomeBaseUrl + "send-email",
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