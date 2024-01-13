class pageModel {
    constructor() {
        this.currentComponent = 'home-component';
    }
}

class homeModel {
    constructor() {
        this.currentBackground = '';
    }
}
class adminDashboardModel {
    constructor() {
        this.imageList = [new imageModel()]
        this.image = new imageModel();
    }
}

class imageModel {
    constructor() {
        this.imageId = '';
        this.imageName = '';
        this.imageLocation = '';
        this.imageFile = null;
        this.imageTypeId = '';
        this.isSelected = false;            ;
    }
}

Vue.component('home-component',
    function (resolve, reject) {
        $.get('./html/index.html').then(function (res) {
            resolve({
                template: res,
                data: function () {
                    return {
                        page: new homeModel()
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
                        window.location = "/#services";
                    },
                    goToContactUs: function () {
                        window.location = "/#contact";
                    },
                    goToBooking: function () {
                        window.location = "/#booking";
                    },
                    getCurrentBackground: function () {
                        var currentObj = this;
                        var actionRequest = getRequest(apiControlHomeUrl.currentBackground);
                        actionRequest.done(function (response) {
                            currentObj.page.currentBackground = response.data.imageLocation;
                        })
                    }
                },
                mounted: function () {
                    getYear(),
                    this.getCurrentBackground();
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
                    },
                    getImageList: function () {
                        let currentObj = this;
                        var actionRequest = getRequest(apiControlAdminUrl.backgroundImageList);

                        actionRequest.done(function (response) {
                            currentObj.page.imageList = response.data
                        })
                    },
                    handleFileUpload(event) {
                        let currentObj = this;
                        // Get the selected file from the input
                        currentObj.page.image.imageFile = event.target.files[0];
                    },

                    //uploadImage: function () {
                    //    let currentObj = this;
                    //    if (!currentObj.page.image.imageFile) {
                    //        alert('Please select a file.');
                    //        return;
                    //    }

                    //    // Create a FormData object to send the file
                    //    const formData = new FormData();
                    //    formData.append('ImageFile', currentObj.page.image.imageFile);
                    //    var actionRequest = postFileRequest(apiControlAdminUrl.uploadImage, formData)
                    //},

                    uploadLogo: function () {
                        let currentObj = this;
                        currentObj.page.image.imageTypeId = 1;
                        if (!currentObj.page.image.imageFile) {
                            alert('Please select a file.');
                            return;
                        }

                        // Create a FormData object to send the file
                        const formData = new FormData();
                        formData.append('ImageFile', currentObj.page.image.imageFile);
                        formData.append('ImageTypeId', currentObj.page.image.imageTypeId);
                        var actionRequest = postFileRequest(apiControlAdminUrl.uploadImage, formData)
                    },

                    uploadBackgroundCover: function () {
                        let currentObj = this;
                        currentObj.page.image.imageTypeId = 2;
                        if (!currentObj.page.image.imageFile) {
                            alert('Please select a file.');
                            return;
                        }

                        // Create a FormData object to send the file
                        const formData = new FormData();
                        formData.append('ImageFile', currentObj.page.image.imageFile);
                        formData.append('ImageTypeId', currentObj.page.image.imageTypeId);
                        var actionRequest = postFileRequest(apiControlAdminUrl.uploadImage, formData)
                        actionRequest.done(function (response) {
                            currentObj.page.imageList = response.data
                        })
                    },

                    handleSelect: function (index) {
                        let currentObj = this;
                        var newSelectedBg = currentObj.page.imageList[index];
                        var actionRequest = postRequest(apiControlAdminUrl.selectBackground, newSelectedBg.imageId)
                    }
                },
                mounted: function () {
                    this.getImageList();
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

                    };
                },
                methods: {

                },
                mounted: function () {

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

                    };
                },
                methods: {

                },
                mounted: function () {

                },
                created: function () {

                }
            })
        })
    })

Vue.component('services-component',
    function (resolve, reject) {
        $.get(htmlUrl.services).then(function (res) {
            resolve({
                template: res,
                data: function () {
                    return {

                    };
                },
                methods: {

                },
                mounted: function () {

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

                    };
                },
                methods: {

                },
                mounted: function () {

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

                    };
                },
                methods: {

                },
                mounted: function () {

                },
                created: function () {

                }
            })
        })
    })