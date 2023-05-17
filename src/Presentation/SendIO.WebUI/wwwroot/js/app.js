window.clipboardCopy = {
    copyText: function (text) {
        navigator.clipboard.writeText(text).then(function () {
            
        })
            .catch(function (error) {
               
            });
    }
};

function downloadFile(link) {
    window.open(link, '_blank');
}

function openModal(modalname) {
    $(modalname).modal('show');
}