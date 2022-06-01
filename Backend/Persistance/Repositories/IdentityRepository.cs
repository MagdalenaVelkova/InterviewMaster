using InterviewMaster.Application.Services;
using InterviewMaster.Domain.Identity;
using InterviewMaster.Persistance.Extensions;
using InterviewMaster.Persistance.Models;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewMaster.Persistance.Repositories
{
    public class IdentityRepository : BaseRepository<UserIdentityDTO>, IIdentityRepository
    {
        private readonly IIdGenerator idGenerator;

        public IdentityRepository(IMongoDatabase database, IIdGenerator idGenerator) : base(database)
        {
            this.idGenerator = idGenerator;
        }

        public const string CollectionName = "Identity";
        public override string DbCollectionName => CollectionName;
        public async Task<string> CreateIdentity(UserAuth userAuth)
        {
            var entity = new UserIdentityDTO
            {
                Id = idGenerator.Generate(),
                Email = userAuth.Email,
                PasswordHash = userAuth.PasswordHash,
                PasswordSalt = userAuth.PasswordSalt,
            };

            try
            {
                await Collection.InsertOneAsync(entity);
            }
            catch (Exception e)
            {

                //log exception
                return null;
            }


            return entity.Id;
        }

        public bool UserCredentialsExists(string id)
        {
            return Query().Any(x => x.Id == id);
        }

        public UserAuth GetUserIdentity(Credentials credentials)
        {
            var userIdentity = Query()
                .Where(x => x.Email == credentials.Email)
                .Select(dto => new UserAuth
                {
                    Id = dto.Id,
                    Email = dto.Email,
                    PasswordHash = dto.PasswordHash,
                    PasswordSalt = dto.PasswordSalt,
                }).FirstOrDefault();

            if (userIdentity != null)
            {
                return userIdentity;
            }
            return null;
        }
        protected override void InitialiseIndecies()
        {
            base.InitialiseIndecies();
            var indexOptions = new CreateIndexOptions() { Unique = true };
            var indexKeys = Builders<UserIdentityDTO>.IndexKeys.Ascending(x => x.Email);
            var indexModel = new CreateIndexModel<UserIdentityDTO>(indexKeys, indexOptions);
            Collection.Indexes.CreateOne(indexModel);
        }

        public bool UserEmailExists(string email)
        {
            return Query().Any(x => x.Email == email);
        }
    }
}
