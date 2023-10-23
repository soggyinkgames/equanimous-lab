mergeInto(LibraryManager.library, 
{
    Alert: function () {
        window.alert("lets do something silly!")
    },

    ToWebPage: function (pageString) {
        window.location.assign("https://anai.netlify.app/" +Pointer_stringify(pageString))
    },

})