var GetUserInfoLib = {
    // 서버에서 Data받아서 UserData를 JSON형태로 반환
    GetUserData: function() {
        var user = JSON.stringify({
            "token":"eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjE1NDUzMTQ5NzIsInR5cGUiOiJJTkRWIiwiaWQiOiIxMzQzMjA0MzIyNDQzMjY1Iiwic2Vzc2lvbklkIjoiNjQzYjFhMjctOTMwMi00OTIzLThhYTgtNDdlNDIzMjY2ZWYyIiwiYXV0aExldmVsIjoxLCJyb2xlcyI6W10sInN1YnNjcmlwdGlvbiI6eyJzdWJzY3JpcHRpb25JZCI6IjE0MDgzMzIzNjA1NjU3NjIiLCJlbmREYXRlIjoiMjAxOS0wMS0xMCIsImFjdGl2ZSI6dHJ1ZX0sInJlYWRPbmx5IjpmYWxzZSwiaWF0IjoxNTQ1MjkzMzcyfQ.93IzIWKwOpIOT-PpKlvy9IfWHMZhNeSxaHosZWWEjfI",
            "userId":"1343204322443265",
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

mergeInto(LibraryManager.library, GetUserInfoLib);