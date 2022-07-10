namespace Project.TofuGirl
{
    /// <summary>
    /// 相机跟随样式
    /// </summary>
    public enum EnumCameraFollow
    {
        Rocket,
        Stage,
        Girl
    }
    /// <summary>
    /// 游戏模式
    /// </summary>
    public enum EnumGameModel
    {
        /// <summary>
        /// 正常模式
        /// </summary>
        Noraml,
        /// <summary>
        /// 重力模式
        /// </summary>
        Grvaity
    }
    /// <summary>
    /// 木条移动类型
    /// </summary>
    public enum EnumBattenMove
    {
        /// <summary>
        /// 左 value=0
        /// </summary>
        Left,
        /// <summary>
        /// 右 value=1
        /// </summary>
        Right
    }
    /// <summary>
    /// 豆腐类型
    /// </summary>
    public enum EnumTofu
    {
        /// <summary>
        /// 正常豆腐
        /// </summary>
        PuTong,
        /// <summary>
        /// 道具豆腐
        /// </summary>
        DaoJu,
        /// <summary>
        /// 特殊豆腐
        /// </summary>
        TeShu,
        /// <summary>
        /// 金色特殊豆腐
        /// </summary>
        JinSeTeShu,
    }

    /// <summary>
    /// 实体类型
    /// </summary>
    public enum EnumEntity
    {
        /// <summary>
        /// 女孩
        /// </summary>
        Girl,
        /// <summary>
        /// 木条
        /// </summary>
        Batten,
        /// <summary>
        /// 豆腐
        /// </summary>
        Tofu,
        /// <summary>
        /// 相机
        /// </summary>
        Camera,
        /// <summary>
        /// 火箭
        /// </summary>
        Rocket,
        /// <summary>
        /// 舞台
        /// </summary>
        Stage
    }
}