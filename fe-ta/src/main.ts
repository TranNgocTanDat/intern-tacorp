import { createApp } from "vue";
import "./style.css";
import App from "./App.vue";
import router from "./router/Router";
import * as ElementPlusIconsVue from "@element-plus/icons-vue";

import VXETable from 'vxe-table'
import 'vxe-table/lib/style.css'


// element-plus
import ElementPlus from "element-plus";
import "element-plus/dist/index.css";

import VxeUI from 'vxe-pc-ui'
import 'vxe-pc-ui/lib/style.css'

const app = createApp(App);
app.use(router);
app.use(VXETable)
app.use(VxeUI)   
app.use(ElementPlus);
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
  app.component(key, component);
}
app.mount("#app");


