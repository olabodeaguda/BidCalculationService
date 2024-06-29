import { reactive } from "vue";

const savedState = JSON.parse(localStorage.getItem("access")) || {};

export const state = reactive({
  isLoggedIn: !!savedState.token,
  username: savedState.userName || "",
  token: savedState.token || "",
});

export const setLoginState = (loginState, username, token) => {
  const key = "access";
  state.isLoggedIn = loginState;
  state.username = username;
  state.token = token;
  if (loginState) {
    localStorage.setItem(
      key,
      JSON.stringify({
        userName: username,
        token: token,
      })
    );
  } else {
    localStorage.removeItem(key);
  }
};
