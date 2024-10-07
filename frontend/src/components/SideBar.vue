<template>
    <v-app-bar color="primary" prominent>
      <v-app-bar-nav-icon variant="text" @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>My files</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn icon="mdi-dots-vertical" variant="text"></v-btn>
    </v-app-bar>
  
    <v-navigation-drawer v-model="drawer" absolute temporary>
      <v-list>
        <v-list-item prepend-avatar="https://randomuser.me/api/portraits/women/85.jpg" subtitle="sandra_a88@gmail.com" title="Sandra Adams"></v-list-item>
      </v-list>
  
      <v-divider></v-divider>
  
      <v-list density="compact" nav>
        <v-list-item prepend-icon="mdi-folder" title="My Files" value="myfiles"></v-list-item>
        <v-list-item prepend-icon="mdi-account-multiple" title="Shared with me" value="shared"></v-list-item>
        <v-list-item prepend-icon="mdi-star" title="Starred" value="starred"></v-list-item>
      </v-list>
    </v-navigation-drawer>
  
    <v-main style="height: 500px;">
      <v-card-text>
        The navigation drawer will appear on the left side of the screen.
      </v-card-text>
    </v-main>
  </template>
  
  <script setup>
  import { ref } from 'vue'
  
  const drawer = ref(false)
  </script>
  