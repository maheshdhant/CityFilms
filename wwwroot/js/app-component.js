class pageModel {
    constructor() {
        this.currentComponent = 'home-component';
        this.logoLocation = '';
    }
}

class homeModel extends pageModel {
    constructor() {
        super({

        });
        this.partnerInfo = [new partnerModel()];
        this.companyProfile = new companyProfileModel();
        this.mail = new emailModel();
    }
}
class adminDashboardModel {
    constructor() {
        this.imageList = [new imageModel()];
        this.logo = new imageModel();
        this.background = new imageModel();
        this.partner = new partnerModel();
        this.partnerInfo = [new partnerModel()];
        this.selectedPartner = new partnerModel();
        this.companyProfile = new companyProfileModel();
    }
}
class partnerMenuModel extends pageModel {
    constructor() {
        super({});
    }
}
class aboutModel extends pageModel {
    constructor() {
        super({});
        this.mail = new emailModel();
    }
}
class bookingModel extends pageModel {
    constructor() {
        super({});
    }
}
class contactModel extends pageModel {
    constructor() {
        super({});
        this.mail = new emailModel();
    }
}
class serviceModel extends pageModel {
    constructor() {
        super({});
    }
}
class companyProfileModel{
    constructor() {
        this.companyProfileId = 0;
        this.companyName = "Company Name";
        this.companyAddress= "Company Address";
        this.companyMail = "Company Mail";
        this.companyPhone = "Company Phone";
        this.companyTagline = "Company Tagline";
    }
}
class imageModel {
    constructor() {
        this.imageId = '';
        this.imageName = '';
        this.imageLocation = '';
        this.imageFile = null;
        this.imageTypeId = '';
        this.isSelected = false;
    }
}

class partnerModel {
    constructor() {
        this.partnerId = '';
        this.partnerName = '';
        this.partnerDescription = '';
        this.partnerPhone = '';
        this.partnerEmail = '';
        this.partnerWebsite = '';
        this.partnerImage = new imageModel();
        this.partnerImageLocation = '';
    }
}
class emailModel {
    constructor() {
        this.firstName = '';
        this.lastName = '';
        this.email = '';
        this.phone = '';
        this.subject = '';
        this.message = '';
    }
}

Vue.component('home-component',
    function (resolve, reject) {
        $.get('./html/index.html').then(function (res) {
            resolve({
                template: res,
                data: function () {
                    return {
                        page: new homeModel(),
                        backgroundImage: '',
                    };
                },
                methods: {
                    goToAdmin: function () {
                        window.location = "/#admin/dashboard";
                    },
                    goToHome: function () {
                        window.location = "/#index";
                    },
                    goToPartners: function () {
                        window.location = "/#partners";
                    },
                    goToAboutUs: function () {
                        window.location = "/#about";
                    },
                    goToServices: function () {
                        window.location = "/#service";
                    },
                    goToContactUs: function () {
                        window.location = "/#contact";
                    },
                    goToBooking: function () {
                        window.location = "/#booking";
                    },
                    getCompanyLogo: function () {
                        var currentObj = this;
                        var actionRequest = getRequest(apiControlHomeUrl.logo);
                        actionRequest.done(function (response) {
                            currentObj.page.logoLocation = response.data;
                        })
                    },
                    getCurrentBackground: function () {
                        var currentObj = this;
                        var actionRequest = getRequest(apiControlHomeUrl.currentBackground);
                        actionRequest.done(function (response) {
                            currentObj.backgroundImage = response.data;
                        })
                    },
                    getPartnerInfo: function () {
                        var currentObj = this;
                        var actionRequest = getRequest(apiControlHomeUrl.partnerInfo)
                        actionRequest.done(function (response) {
                            currentObj.page.partnerInfo = response.data
                        })
                    },
                    getCompanyProfile: function () {
                        var currentObj = this;
                        var actionRequest = getRequest(apiControlAdminUrl.getCompanyProfile)
                        actionRequest.done(function (response) {
                            if (typeof (response.data) != "string") {
                                currentObj.page.companyProfile = response.data;
                            }
                        });
                    },
                    sendEmail: function () {
                        debugger
                        var currentObj = this;
                        var actionRequest = postRequest(apiControlHomeUrl.sendEmail, currentObj.page.mail);
                        actionRequest.done(function (response) {
                            currentObj.page.mail = new emailModel();
                        })
                    }
                },
                mounted: function () {
                    getYear();
                    this.getCompanyLogo();
                    this.getCurrentBackground();
                    this.getPartnerInfo();
                    this.getCompanyProfile();
                },
            })
        })
    })

Vue.component('admin-dashboard-component',
    function (resolve, reject) {
        $.get(htmlControlAdminUrl.adminDashboard).then(function (res) {
            resolve({
                template: res,
                data: function () {
                    return {
                        page: new adminDashboardModel(),
                    };
                },
                methods: {
                    goToHome: function () {
                        window.location = "/#index";
                        window.location.reload();
                    },
                    getImageList: function () {
                        let currentObj = this;
                        var actionRequest = getRequest(apiControlAdminUrl.backgroundImageList);

                        actionRequest.done(function (response) {
                            currentObj.page.imageList = response.data
                        })
                    },
                    handleLogoUpload(event) {
                        let currentObj = this;
                        // Get the selected file from the input
                        currentObj.page.logo.imageFile = event.target.files[0];
                    },
                    handleBackgroundUpload(event) {
                        let currentObj = this;
                        // Get the selected file from the input
                        currentObj.page.background.imageFile = event.target.files[0];
                    },
                    handlePartnerImageUpload(event) {
                        let currentObj = this;
                        // Get the selected file from the input
                        currentObj.page.partner.partnerImage.imageFile = event.target.files[0];
                    },
                    handlePartnerImageUpdate(event) {
                        let currentObj = this;
                        // Get the selected file from the input
                        currentObj.page.selectedPartner.partnerImage.imageFile = event.target.files[0];
                    },
                    uploadLogo: function () {
                        let currentObj = this;
                        currentObj.page.logo.imageTypeId = 1;
                        if (!currentObj.page.logo.imageFile) {
                            alert('Please select a file.');
                            return;
                        }

                        // Create a FormData object to send the file
                        const formData = new FormData();
                        formData.append('ImageFile', currentObj.page.logo.imageFile);
                        formData.append('ImageTypeId', currentObj.page.logo.imageTypeId);
                        var actionRequest = postFileRequest(apiControlAdminUrl.uploadImage, formData)
                    },
                    uploadBackgroundImage: function () {
                        let currentObj = this;
                        currentObj.page.background.imageTypeId = 2;
                        if (!currentObj.page.background.imageFile) {
                            alert('Please select a file.');
                            return;
                        }

                        // Create a FormData object to send the file
                        const formData = new FormData();
                        formData.append('ImageFile', currentObj.page.background.imageFile);
                        formData.append('ImageTypeId', currentObj.page.background.imageTypeId);
                        var actionRequest = postFileRequest(apiControlAdminUrl.uploadImage, formData)
                        actionRequest.done(function (response) {
                            currentObj.page.imageList = response.data
                        })
                    },
                    handleSelect: function (index) {
                        let currentObj = this;
                        var newSelectedBg = currentObj.page.imageList[index];
                        var actionRequest = postRequest(apiControlAdminUrl.selectBackground + newSelectedBg.imageId)
                        actionRequest.done(function () {

                        });
                    },
                    deleteBackgroundImage: function (index) {
                        let currentObj = this;
                        var imgToDelete = currentObj.page.imageList[index];
                        var actionRequest = postRequest(apiControlAdminUrl.deleteBackground + imgToDelete.imageId)
                        actionRequest.done(function (response) {
                            currentObj.page.imageList = response.data
                        })
                    },
                    uploadPartnerInfo: function () {
                        let currentObj = this;
                        currentObj.page.partner.partnerImage.imageTypeId = 3;
                        if (!currentObj.page.partner.partnerImage.imageFile) {
                            alert('Please select a file.');
                            return;
                        }

                        // Create a FormData object to send the file
                        const formData = new FormData();
                        formData.append("PartnerImage", currentObj.page.partner.partnerImage.imageFile);
                        formData.append('PartnerName', currentObj.page.partner.partnerName);
                        formData.append('PartnerDescription', currentObj.page.partner.partnerDescription);
                        formData.append('PartnerPhone', currentObj.page.partner.partnerPhone);
                        formData.append('PartnerEmail', currentObj.page.partner.partnerEmail);
                        formData.append('PartnerWebsite', currentObj.page.partner.partnerWebsite);
                        var actionRequest = postFileRequest(apiControlAdminUrl.addPartnerInfo, formData)
                        actionRequest.done(function (response) {
                            currentObj.page.partner.partnerImage.imageFile = null;
                            currentObj.page.partner = new partnerModel();
                        })
                    },
                    getPartnerInfo: function () {
                        let currentObj = this;
                        var actionRequest = getRequest(apiControlAdminUrl.getPartnerInfo)
                        actionRequest.done(function (response) {
                            currentObj.page.partnerInfo = response.data
                        })
                    },
                    loadModal: function (partner) {
                        var currentObj = this;
                        currentObj.page.selectedPartner.partnerImageLocation = partner.partnerImageLocation
                        currentObj.page.selectedPartner.partnerName = partner.partnerName
                        currentObj.page.selectedPartner.partnerDescription = partner.partnerDescription
                        currentObj.page.selectedPartner.partnerPhone = partner.partnerPhone
                        currentObj.page.selectedPartner.partnerEmail = partner.partnerEmail
                        currentObj.page.selectedPartner.partnerWebsite = partner.partnerWebsite
                        currentObj.page.selectedPartner.partnerId = partner.partnerId
                    },
                    updatePartnerInfo: function (id) {
                        var currentObj = this;

                        // Create a FormData object to send the file
                        const formData = new FormData();
                        formData.append("PartnerImage", currentObj.page.selectedPartner.partnerImage.imageFile);
                        formData.append('PartnerName', currentObj.page.selectedPartner.partnerName);
                        formData.append('PartnerDescription', currentObj.page.selectedPartner.partnerDescription);
                        formData.append('PartnerPhone', currentObj.page.selectedPartner.partnerPhone);
                        formData.append('PartnerEmail', currentObj.page.selectedPartner.partnerEmail);
                        formData.append('PartnerWebsite', currentObj.page.selectedPartner.partnerWebsite);
                        formData.append('PartnerId', id);
                        debugger
                        var actionRequest = postFileRequest(apiControlAdminUrl.editPartnerInfo, formData)
                        actionRequest.done(function (response) {
                            currentObj.page.partnerInfo = response.data
                            currentObj.page.partner.partnerImage.imageFile = null;
                            currentObj.page.partner = new partnerModel();
                            currentObj.page.selectedPartner = new partnerModel();
                            currentObj.page.selectedPartner.partnerImage.imageFile = null
                        })
                    },
                    getCompanyProfile: function () {
                        var currentObj = this;
                        var actionRequest = getRequest(apiControlAdminUrl.getCompanyProfile)
                        actionRequest.done(function (response) {
                            if (typeof (response.data) != "string") {
                                currentObj.page.companyProfile = response.data;
                            }
                        });
                    },
                    updateCompanyProfile: function () {
                        var currentObj = this;
                        var actionRequest = postRequest(apiControlAdminUrl.updateCompanyProfile, this.page.companyProfile)
                        actionRequest.done(function (response) {
                            currentObj.page.companyProfile = response.data;
                        });
                    }
                },
                mounted: function () {
                    this.getImageList();
                    this.getCompanyProfile();
                    this.getPartnerInfo();
                },
            })
        })
    })

Vue.component('partners-component',
    function (resolve, reject) {
        $.get(htmlUrl.partners).then(function (res) {
            resolve({
                template: res,
                data: function () {
                    return {
                        page: new partnerMenuModel(),
                    };
                },
                methods: {
                    goToHome: function () {
                        window.location = "/#index";
                    },
                    goToPartners: function () {
                        window.location = "/#partners";
                    },
                    goToAboutUs: function () {
                        window.location = "/#about";
                    },
                    goToServices: function () {
                        window.location = "/#service";
                    },
                    goToContactUs: function () {
                        window.location = "/#contact";
                    },
                    goToBooking: function () {
                        window.location = "/#booking";
                    },
                    getCompanyLogo: function () {
                        var currentObj = this;
                        var actionRequest = getRequest(apiControlHomeUrl.logo);
                        actionRequest.done(function (response) {
                            currentObj.page.logoLocation = response.data;
                        })
                    },
                },
                mounted: function () {
                    this.getCompanyLogo()
                },
                created: function () {

                }
            })
        })
    })

Vue.component('about-component',
    function (resolve, reject) {
        $.get(htmlUrl.about).then(function (res) {
            resolve({
                template: res,
                data: function () {
                    return {
                        page: new aboutModel(),
                    };
                },
                methods: {
                    goToHome: function () {
                        window.location = "/#index";
                    },
                    goToPartners: function () {
                        window.location = "/#partners";
                    },
                    goToAboutUs: function () {
                        window.location = "/#about";
                    },
                    goToServices: function () {
                        window.location = "/#service";
                    },
                    goToContactUs: function () {
                        window.location = "/#contact";
                    },
                    goToBooking: function () {
                        window.location = "/#booking";
                    },
                    getCompanyLogo: function () {
                        var currentObj = this;
                        var actionRequest = getRequest(apiControlHomeUrl.logo);
                        actionRequest.done(function (response) {
                            currentObj.page.logoLocation = response.data;
                        })
                    },
                },
                mounted: function () {
                    this.getCompanyLogo();
                },
                created: function () {

                }
            })
        })
    })

Vue.component('service-component',
    function (resolve, reject) {
        $.get(htmlUrl.service).then(function (res) {
            resolve({
                template: res,
                data: function () {
                    return {
                        page: new serviceModel(),
                    };
                },
                methods: {
                    goToHome: function () {
                        window.location = "/#index";
                    },
                    goToPartners: function () {
                        window.location = "/#partners";
                    },
                    goToAboutUs: function () {
                        window.location = "/#about";
                    },
                    goToServices: function () {
                        window.location = "/#service";
                    },
                    goToContactUs: function () {
                        window.location = "/#contact";
                    },
                    goToBooking: function () {
                        window.location = "/#booking";
                    },
                    getCompanyLogo: function () {
                        var currentObj = this;
                        var actionRequest = getRequest(apiControlHomeUrl.logo);
                        actionRequest.done(function (response) {
                            currentObj.page.logoLocation = response.data;
                        })
                    },
                },
                mounted: function () {
                    this.getCompanyLogo()
                },
                created: function () {

                }
            })
        })
    })

Vue.component('contact-component',
    function (resolve, reject) {
        $.get(htmlUrl.contact).then(function (res) {
            resolve({
                template: res,
                data: function () {
                    return {
                        page: new contactModel(),
                    };
                },
                methods: {
                    goToHome: function () {
                        window.location = "/#index";
                    },
                    goToPartners: function () {
                        window.location = "/#partners";
                    },
                    goToAboutUs: function () {
                        window.location = "/#about";
                    },
                    goToServices: function () {
                        window.location = "/#service";
                    },
                    goToContactUs: function () {
                        window.location = "/#contact";
                    },
                    goToBooking: function () {
                        window.location = "/#booking";
                    },
                    getCompanyLogo: function () {
                        var currentObj = this;
                        var actionRequest = getRequest(apiControlHomeUrl.logo);
                        actionRequest.done(function (response) {
                            currentObj.page.logoLocation = response.data;
                        })
                    },
                    emailSender: function () {
                        currentObj = this;
                    }
                },
                mounted: function () {
                    this.getCompanyLogo()
                },
                created: function () {

                }
            })
        })
    })

Vue.component('booking-component',
    function (resolve, reject) {
        $.get(htmlUrl.booking).then(function (res) {
            resolve({
                template: res,
                data: function () {
                    return {
                        page: new bookingModel(),
                    };
                },
                methods: {
                    goToHome: function () {
                        window.location = "/#index";
                    },
                    goToPartners: function () {
                        window.location = "/#partners";
                    },
                    goToAboutUs: function () {
                        window.location = "/#about";
                    },
                    goToServices: function () {
                        window.location = "/#service";
                    },
                    goToContactUs: function () {
                        window.location = "/#contact";
                    },
                    goToBooking: function () {
                        window.location = "/#booking";
                    },
                    getCompanyLogo: function () {
                        var currentObj = this;
                        var actionRequest = getRequest(apiControlHomeUrl.logo);
                        actionRequest.done(function (response) {
                            currentObj.page.logoLocation = response.data;
                        })
                    },
                },
                mounted: function () {
                    this.getCompanyLogo();
                },
                created: function () {

                }
            })
        })
    })