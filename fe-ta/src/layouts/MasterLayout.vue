<script setup lang="ts">
import { RouterView } from "vue-router";
import Header from "@/components/Header/Header.vue";

import Footer from "@/components/Footer/Footer.vue";
</script>

<template>
  <div class="header">
    <Header />
  </div>
  <div class="main">
    <RouterView />
  </div>
  <div class="footer">
  
    <Footer />
  </div>
</template>

<style scoped>
.header {
  position: fixed;
  top: 0;
  left: 0;
  z-index: 1000;
  width: 100%;
  height: 70px;
  background-color: transparent;
}
.main {
  min-height: calc(100vh - 70px);
  /* padding: 0 20px; */
}
.footer {
  text-align: center;
  padding: 10px;
}
</style>
