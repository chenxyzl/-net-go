using common;
namespace cc
{
    public interface TestRpc
    {
        int TestHandler(I1 ii);
        Task<int> TestTaskHandler(I1 ii);
    }
}