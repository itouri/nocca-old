mergeInto(LibraryManager.library, {

  _GetURL: function () {
    var returnStr = location.href;
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  _Alert: function (str) {
	window.alert(Pointer_stringify(str));
  }
});