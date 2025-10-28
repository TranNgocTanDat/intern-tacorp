<script setup lang="ts">
import { RouterView } from "vue-router";
// import Header from "@/components/Header/Header.vue";

// import Footer from "@/components/Footer/Footer.vue";
</script>

<template>
  <!-- <div class="header">
    <Header />
  </div> -->
  <div class="main">
    <RouterView />
  </div>
  <!-- <div class="footer">
  
    <Footer />
  </div> -->
</template>

<style scoped>
.header {
  position: fixed;
  top: 0;
  left: 0;
  z-index: 1000;
  width: 100%;
  height: 66px;
  background-color: transparent;
  border-bottom: 2px solid black;
  backdrop-filter: blur(100px);
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.5);
}
.main {
  padding-top: 66px;
  min-height: calc(100vh - 66px);
  /* padding: 0 20px; */
}
.footer {
  height: auto;
  padding: 2rem 4rem;
}
</style>
