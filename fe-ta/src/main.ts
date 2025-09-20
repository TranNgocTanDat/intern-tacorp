import { createApp } from "vue";
import "./style.css";
import App from "./App.vue";
import router from "./router/Router";
import * as ElementPlusIconsVue from "@element-plus/icons-vue";

import VXETable from "vxe-table";
import "vxe-table/lib/style.css";

// element-plus
import ElementPlus from "element-plus";
import "element-plus/dist/index.css";

import VxeUI from "vxe-pc-ui";
import "vxe-pc-ui/lib/style.css";

import { createI18n } from "vue-i18n";
import vi from "./i18n/vxe-vi";

const i18n = createI18n({
  locale: "vi",
  messages: {
    vi,
  },
});

const app = createApp(App);

// Truyền hàm i18n cho VXETable
VXETable.setConfig({
  i18n: (key: string, args?: any) => i18n.global.t(key, args),
});

app.use(i18n);
app.use(router);
app.use(VXETable);
app.use(VxeUI);
app.use(ElementPlus);
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
  app.component(key, component);
}
app.mount("#app");
