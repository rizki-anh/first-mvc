using  MyApp.Repository;
namespace servicerole
{
    public class Role
    {
        private readonly UserRepository _userRepository;

        public Role(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public string GetRole(int? nutpk)
        {
              var nuptkList = _userRepository.GetAllNuptkAsync().Result;
            if (nutpk == null || nutpk == 0 || nuptkList?.Contains(nutpk.Value) == false)
            {
                return "user";
            }
            else
            {
                return "guru";
            }

        }
    }
    }
