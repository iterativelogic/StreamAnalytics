import { createStore, applyMiddleware, combineReducers } from "redux";
import { appReducers } from "./reducers";
import createSagaMiddleware from "redux-saga";
import { rootSaga } from "./sagas";

const bootstrapStore = () => {
  console.log("Initializing store started...");
  const sagaMiddleware = createSagaMiddleware();
  const store = createStore(
    combineReducers(appReducers),
    applyMiddleware(sagaMiddleware)
  );
  sagaMiddleware.run(rootSaga);
  console.log("Initializing store completed...");
  return store;
};

export const store = bootstrapStore();
export type IAppState = ReturnType<typeof store.getState>;
