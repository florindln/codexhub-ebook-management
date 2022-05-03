import Cookies from "js-cookie";
import jwt_decode from "jwt-decode";
import { LoginRequest } from "./APIService";

class AuthService {
  async Login(email, password) {
    const response = await LoginRequest(email, password);
    if (response.status === 200) {
      // console.log(response.data);
      Cookies.set("Authorization", response.data.token);
      return response.data;
    } else {
      return null;
    }
  }

  logout() {
    Cookies.remove("Authorization");
  }

  getCurrentUser() {
    let token = Cookies.get("Authorization");
    if (token !== undefined) {
      let decoded = jwt_decode(token);

      if (decoded.exp * 1000 < Date.now()) {
        this.logout();
        return { id: "", token: "", role: "" };
      }
      return { id: decoded.id, token: token, role: decoded.permissionRole };
    } else {
      return { id: "", token: "", role: "" };
    }
  }

  getUserRole() {
    try {
      return this.getCurrentUser().role;
    } catch (error) {
      return "";
    }
  }
}
export default new AuthService();
