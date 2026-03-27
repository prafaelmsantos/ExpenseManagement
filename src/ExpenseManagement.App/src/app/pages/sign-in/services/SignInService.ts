import { BASE_API_URL } from "../../../config/Variables";
import { postData } from "../../../services/BaseService";

import { IUserLogin } from "../models/UserLogin";
import { IUserToken } from "../models/UserToken";

const loginUser = async (userLogin: IUserLogin): Promise<IUserToken> =>
  await postData<IUserLogin, IUserToken>(
    `${BASE_API_URL}/auth/login`,
    userLogin,
    true
  );

export { loginUser };
