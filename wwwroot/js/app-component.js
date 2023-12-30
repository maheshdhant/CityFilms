class pageModel {
    constructor() {
        this.currentComponent = 'home-component';
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

                },
                mounted: function () {
                    getYear()
                },
            })
        })
    })