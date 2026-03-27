import { IBaseResponse } from "../models/BaseResponse";

interface IErrorResponse {
  message?: string;
}

export async function getErrorMessage(
  response: Response,
  isLogin: boolean = false
): Promise<string> {
  try {
    if (!isLogin && response.status === 401) {
      localStorage.removeItem("token");
      localStorage.removeItem("user");
      window.location.href = "/";
    }

    return (
      (await (response.json() as Promise<IErrorResponse>)).message ||
      response.statusText ||
      "error"
    );
  } catch {
    return response.statusText || "error";
  }
}

export const getSessionHeaders = (contentType = "application/json") => {
  const token = localStorage.getItem("token");
  return {
    "Content-type": contentType,
    Authorization: token ? `Bearer ${token}` : ""
  };
};

export async function getData<T>(endpoint: string): Promise<T> {
  const response = await fetch(endpoint, {
    method: "GET",
    headers: getSessionHeaders()
  });
  if (!response.ok) {
    throw new Error(await getErrorMessage(response));
  }

  return (await response.json()) as Promise<T>;
}

export async function postData<A, B>(
  endpoint: string,
  body: A,
  isLogin: boolean = false
): Promise<B> {
  const response = await fetch(endpoint, {
    method: "POST",
    headers: getSessionHeaders(),
    body: JSON.stringify(body)
  });
  if (!response.ok) {
    throw new Error(await getErrorMessage(response, isLogin));
  }
  return (await response.json()) as Promise<B>;
}

export async function putData<A, B>(endpoint: string, body: A): Promise<B> {
  const response = await fetch(endpoint, {
    method: "PUT",
    headers: getSessionHeaders(),
    body: JSON.stringify(body)
  });
  if (!response.ok) {
    throw new Error(await getErrorMessage(response));
  }
  return (await response.json()) as Promise<B>;
}

export async function postDeleteData(
  endpoint: string,
  body: string[]
): Promise<IBaseResponse[]> {
  const response = await fetch(endpoint, {
    method: "POST",
    headers: getSessionHeaders(),
    body: JSON.stringify(body)
  });
  if (!response.ok) {
    throw new Error(response.statusText);
  }
  return (await response.json()) as Promise<IBaseResponse[]>;
}
