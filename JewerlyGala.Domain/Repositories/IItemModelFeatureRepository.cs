namespace JewerlyGala.Domain.Repositories
{
    public interface IItemModelFeatureRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<int> CreateAsync(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<int> ExistsByName(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idFeature"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(int idFeature, string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idModel"></param>
        /// <param name="idFeature"></param>
        /// <returns></returns>
        Task LinkFeatureToModel(int idModel, int idFeature);
    }
}
