using System;
using System.Collections.Generic;

public class FacialFeatures
{
    public string EyeColor { get; }
    public decimal PhiltrumWidth { get; }

    public FacialFeatures(string eyeColor, decimal philtrumWidth)
    {
        EyeColor = eyeColor;
        PhiltrumWidth = philtrumWidth;
    }

    public override bool Equals(object obj)
    {
        if (obj is FacialFeatures other)
        {
            return EyeColor == other.EyeColor &&
                   PhiltrumWidth == other.PhiltrumWidth;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(EyeColor, PhiltrumWidth);
    }
}

public class Identity
{
    public string Email { get; }
    public FacialFeatures FacialFeatures { get; }

    public Identity(string email, FacialFeatures facialFeatures)
    {
        Email = email;
        FacialFeatures = facialFeatures;
    }

    public override bool Equals(object obj)
    {
        if (obj is Identity other)
        {
            return Email == other.Email &&
                   Equals(FacialFeatures, other.FacialFeatures);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Email, FacialFeatures);
    }
}

public class Authenticator
{
    private readonly HashSet<Identity> registeredIdentities = new HashSet<Identity>();

    private static readonly Identity adminIdentity =
        new Identity("admin@exerc.ism", new FacialFeatures("green", 0.9m));

    public static bool AreSameFace(FacialFeatures faceA, FacialFeatures faceB)
    {
        return Equals(faceA, faceB);
    }

    public bool IsAdmin(Identity identity)
    {
        return adminIdentity.Equals(identity);
    }

    public bool Register(Identity identity)
    {
        return registeredIdentities.Add(identity);
    }

    public bool IsRegistered(Identity identity)
    {
        return registeredIdentities.Contains(identity);
    }

    public static bool AreSameObject(Identity identityA, Identity identityB)
    {
        return ReferenceEquals(identityA, identityB);
    }
}
