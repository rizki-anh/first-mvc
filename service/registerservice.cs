using MyApp.Repository;
using BCrypt.Net;
using servicerole;

namespace service.registerservice
{
    public class RegisterService
    {
        private readonly UserRepository _userRepository;
        private readonly Role _roleService;
        public RegisterService(UserRepository userRepository)
        {
            _userRepository = userRepository;
            _roleService = new Role(userRepository);
        }

        public void RegisterUser(dto.registerrequestdto.Registerdto userDto)
        {
            if (_userRepository.Getbyusername(userDto.username) != null)
            {
                throw new Exception("Username sudah terdaftar.");
            }
            var Role = _roleService.GetRole(userDto.NUPTK);

            var user = new register.Models.UserRegister
            {
                username = userDto.username,
                NUPTK = userDto.NUPTK,
                role = Role,
                email = userDto.email
            };

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.password);
            user.password = hashedPassword;

            _userRepository.Adduser(user);
        }
    }
}
