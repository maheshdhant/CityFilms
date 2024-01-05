class pageModel {
    constructor() {
        this.currentComponent = 'home-component';
    }
}

class adminDashboardModel {
    constructor() {
        this.logoLocation = '';
        this.image = new imageModel();
    }
}

class imageModel {
    constructor() {
        this.imageName = '';
        this.imageLocation = '';
        this.imageFile = null;
        this.imageTypeId = '';
    }
}

Vue.component('home-component',
    function (resolve, reject) {
        $.get('./html/index.html').then(function (res) {
            resolve({
                template: res,
                data: function () {
                    return {
                        
                    };
                },
                methods: {
                    goToAdmin: function () {
                        window.location = "/#admin/dashboard";
                    }
                },
                mounted: function () {
                    getYear()
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
                        selectedFile: null,
                    };
                },
                methods: {
                    getImageList: function () {
                        let currentObj = this;
                        var response = getRequest(apiControlAdminUrl.imageList);

                        response.done(function (response) {
                            currentObj.page.imageList = response.data
                        })
                    },
                    handleFileUpload(event) {
                        let currentObj = this;
                        // Get the selected file from the input
                        currentObj.page.image.imageFile = event.target.files[0];
                        debugger
                    },

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
                    uploadImage: function () {
                        let currentObj = this;
                        if (!currentObj.page.image.imageFile) {
                            alert('Please select a file.');
                            return;
                        }

                        // Create a FormData object to send the file
                        const formData = new FormData();
                        formData.append('ImageFile', currentObj.page.image.imageFile);
                        var actionRequest = postFileRequest(apiControlAdminUrl.uploadImage, formData)
                    },

                },
                mounted: function () {
                    this.getImageList();
                },
            })
        })
    })