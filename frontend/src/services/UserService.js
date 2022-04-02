import axios from "axios";

const user_Url = process.env.BOOK_API_URL + "/Users";

// Add a request interceptor
// axios.interceptors.request.use((config) => {
//   const user = JSON.parse(localStorage.getItem("book"));

//   if (user && user.Authorization) {
//     const token = user.Authorization;
//     config.headers.Authorization = token;
//   }

//   return config;
// });

class UserService {
  getAllUsers() {
    return axios.get(user_Url);
  }
  getUserById(id) {
    return axios.get(user_Url + "/" + id);
  }
  updateUserById(id, user) {
    return axios.put(user_Url + "/" + id, user);
  }
}

export default new UserService();
