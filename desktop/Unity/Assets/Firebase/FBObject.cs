using System.Collections;
using System.Collections.Generic;

public class FBObject
{
    public string name;
    public Dictionary<string, Dictionary<string, string>> fields;
    public string createTime;
    public string updateTime;
}

public class User : FBObject { }

public class Document : FBObject { }

public class SignInSuccessRes
{
    public string kind;
    public string localId;
    public string email;
    public string displayName;
    public string idToken;
    public string registered;
    public string refreshToken;
    public string expiresIn;
}

public class SignInErrorRes
{
    public SignInResBody error;
}

public class SignInResBody
{
    public int code;
    public string message;
    public List<SignInError> errors;
}

public class SignInError
{
    public string message;
    public string domain;
    public string reason;
}

public class SignInRes
{
    public bool Success;
    public string Uid;
    public string Email;
    public string Token;
    public string RefreshToken;
    public string ExpiresIn;
}

public class SignInReq
{
    public string email;
    public string password;
    public bool returnSecureToken = true;
}

public class SignUpSuccessRes
{
    public string kind;
    public string idToken;
    public string email;
    public string refreshToken;
    public string expiresIn;
    public string localId;
}

public class SignUpErrorBody
{
    public SignUpErrorInner error;
}

public class SignUpErrorInner
{
    public int code;
    public string message;
    public List<Dictionary<string, string>> errors;
}

public class SignUpRes
{
    public bool Success;
    public string Uid;
    public string Email;
    public string ErrorMessage;
}

public class PasswordResetRes
{
    public bool Success;
}

public class PasswordResetReq
{
    public string email;
}

public class PasswordResetSuccessRes
{
    public string kind;
    public string localId;
    public string email;
    public string displayName;
    public string idToken;
    public string registered;
    public string refreshToken;
    public string expiresIn;
}

public class PasswordResetErrorRes
{
    public SignInResBody error;
}
