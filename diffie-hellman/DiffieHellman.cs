using System.Numerics;

public static class DiffieHellman
{
    public static BigInteger PrivateKey(BigInteger primeP)
    {
        return 2;
    }

    public static BigInteger PublicKey(BigInteger primeP, BigInteger primeG, BigInteger privateKey)
    {
        return BigInteger.ModPow(primeG, privateKey, primeP);
    }

    public static BigInteger Secret(BigInteger primeP, BigInteger publicKey, BigInteger privateKey)
    {
        return BigInteger.ModPow(publicKey, privateKey, primeP);
    }
}