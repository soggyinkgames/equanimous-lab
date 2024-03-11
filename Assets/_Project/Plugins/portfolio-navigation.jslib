mergeInto(LibraryManager.library, 
{
    Alert: function () {
        window.alert("lets do something silly!")
    },

    ToWebPage: function (pageString) {
        window.location.assign("https://ian-araya.netlify.app/" +Pointer_stringify(pageString))
    },

})