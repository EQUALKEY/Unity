var GetUserInfoLib = {
    // 서버에서 Data받아서 UserData를 JSON형태로 반환
    GetUserData: function() {
        var user = JSON.stringify({
            "token":"eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjE1NDMxMzg3MDQsInR5cGUiOiJJTkRWIiwiaWQiOiIxMDY4MTgzNjY2NTU2OTI5Iiwic2Vzc2lvbklkIjoiYTRlMTE1MTItMWExNS00MjM5LTllMDYtNTdiYTBkNzE2ZTE0IiwiYXV0aExldmVsIjo5LCJyb2xlcyI6W3sibmFtZSI6InByZW1pdW1fdXNlciIsInBlcm1pc3Npb25zIjpbIlBSRU1JVU1fVVNFUiJdfV0sInN1YnNjcmlwdGlvbiI6eyJzdWJzY3JpcHRpb25JZCI6IjEyNjg5MjI1OTU1NzQ3ODciLCJlbmREYXRlIjoiMjAxOS0wNS0yMSIsImFjdGl2ZSI6dHJ1ZX0sInJlYWRPbmx5IjpmYWxzZSwiaWF0IjoxNTQzMTE3MTA0fQ.L7s4O-Nskr4Q3YWnAn9Yj3uPe7XH3y6ceyAPeEVFsMY",
            "userId":"1068183666556929",
            "host":"https://dev-api.quebon.tv",
            "nickname":"테스트닉네임"
        })
        var user_data = JSON.stringify(user);

        var bufferSize = lengthBytesUTF8(user_data) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(user_data, buffer, bufferSize);
        console.log(user_data);

        return buffer;
    }
};

mergeInto(LibraryManager.library, GetUserInfoLib);