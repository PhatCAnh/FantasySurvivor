namespace ArbanFramework.Config
{
    public interface IConfigReader
    {
        bool HasNext();
        int ReadInt();
        long ReadLong();
        float ReadFloat();
        double ReadDouble();
        string ReadString();
        int[] ReadIntArr();
        float[] ReadFloatArr();
        double[] ReadDoubleArr();
        long[] ReadLongArr();
        string[] ReadStringArr();
    }
}