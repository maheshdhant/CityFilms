class pageModel {
    constructor() {
        this.currentComponent = 'home-component';
    }
}

class adminDashboardModel {
    constructor() {
        this.imageList = [new imageModel()];
    }
}

class imageModel {
    constructor() {
        this.imageName = '';
        this.imageLocation = '';
        this.imageFile = '';
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
                        currentObj.page.imageList[0].imageFile = event.target.files[0];
                        debugger
                    },
                    uploadImage: function () {
                        let currentObj = this;
                        if (!currentObj.page.imageList[0].imageFile) {
                            alert('Please select a file.');
                            return;
                        }

                        // Create a FormData object to send the file
                        const formData = new FormData();
                        formData.append('file', currentObj.page.imageList[0].imageFile);
                        debugger
                        postRequest(apiControlAdminUrl.addImage, currentObj.page.imageList[0])
                    }
                },
                mounted: function () {
                    
                },
            })
        })
    })