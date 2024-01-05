class pageModel {
    constructor() {
        this.currentComponent = 'home-component';
    }
}

class adminDashboardModel {
    constructor() {
        this.imageList = [new imageModel()];
        this.image = new imageModel();
    }
}

class imageModel {
    constructor() {
        this.imageName = '';
        this.imageLocation = '';
        this.imageFile = null;            ;
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
                            currentObj.page.imageList = response
                        })
                    },
                    handleFileUpload(event) {
                        let currentObj = this;
                        // Get the selected file from the input
                        currentObj.page.image.imageFile = event.target.files[0];
                        debugger
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
                        var actionRequest = postFileRequest(apiControlAdminUrl.addImage, formData)
                    },
                    addRecord: function () {
                        let currentObj = this;
                        var form1 = $('#file-form-Document_aa')[0];
                        var formData = new FormData(form1);
                        formData.append("image", currentObj.page.image);
                        var actionRequest = postFileRequest(apiControlAdminUrl.addImage, formData);
                        debugger
                    }
                },
                mounted: function () {
                    //this.getImageList();
                },
            })
        })
    })