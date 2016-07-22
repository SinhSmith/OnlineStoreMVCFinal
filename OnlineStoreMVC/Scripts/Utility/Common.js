var MessageBox = {
    getTemplate:function(){
        var template = "";
        template += "<div id=\"Modal_MessageBox\" class=\"modal fade\" tabindex=\"-1\" role=\"dialog\">";
        template += "        <div class=\"modal-dialog\">";
        template += "            <div class=\"modal-content\">";
        template += "                <div class=\"modal-header\">";
        template += "                    <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;<\/span><\/button>";
        template += "                    <h4 class=\"modal-title\"><\/h4>";
        template += "                <\/div>";
        template += "                <div class=\"modal-body\">";
        template += "                <\/div>";
        template += "                <div class=\"modal-footer\">";
        template += "                    <button type=\"button\" id=\"Btn_OK_MessageBox\" class=\"btn btn-primary\">OK<\/button>";
        template += "                    <button type=\"button\" id=\"Btn_Cancel_MessageBox\" class=\"btn btn-default\" data-dismiss=\"modal\">Cancel<\/button>";
        template += "                <\/div>";
        template += "            <\/div>";
        template += "        <\/div>";
        template += "    <\/div>";

        return template;
    },
    getElement:function(){
        return $("#Modal_MessageBox");
    },
    showMessageBox: function (title,message,callback) {
        this.getElement().find(".modal-title").text(title);
        this.getElement().find(".modal-body").text(message);
        this.getElement().find("#Btn_OK_MessageBox").unbind("click").bind("click", function () {
            if (callback && typeof (callback) == 'function') {
                callback();
            }
            MessageBox.hideMessageBox();
        });
        this.getElement().find("#Btn_Cancel_MessageBox").unbind("click").bind("click", function () {
            MessageBox.hideMessageBox();
        });
        this.getElement().modal('show', { backdrop: true });
    },
    hideMessageBox: function () {
        this.getElement().modal('hide');
    }
}
$.fn.extend({
    initMessageBox: function () {
        var selectedObjects = this;
        $(selectedObjects).each(function () {
            $(this).html(MessageBox.getTemplate());
        });
    }
})
