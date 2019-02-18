
function IsNullOrEmpty(bn) {
	if (bn == null || bn == '') {
		return true;
	}
	return false;
}
layui.use('layer', function () {
    parent.layer.closeAll('loading');
});

