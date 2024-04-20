function showJSToaster(title, message, icon) {
    $.NotificationApp.send(title, message, "bottom-right", "rgba(0,0,0,0.2)", icon)
};
