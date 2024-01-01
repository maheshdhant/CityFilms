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

                    }
                },
                mounted: function () {
                    
                },
            })
        })
    })