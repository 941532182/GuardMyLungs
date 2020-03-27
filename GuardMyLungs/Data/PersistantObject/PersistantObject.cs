namespace Data
{
    /// <summary>
    /// PO的抽象基类，实现了Id属性
    /// </summary>
    public abstract class PersistantObject : IPersistant
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; protected set; } = -1;
    }
}
