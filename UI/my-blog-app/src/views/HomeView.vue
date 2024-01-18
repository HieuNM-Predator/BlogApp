<template>
  <app-header />
  <v-container>
    <v-row>
      <v-col
        lg="4"
        md="4"
        sm="12"
        xs="12"
        v-for="blogPost in blogPosts"
        :key="blogPost.id"
      >
        <v-card>
          <v-img cover height="300" :src="blogPost.featureImageUrl">
            <template v-slot:placeholder>
              <v-row class="fill-height ma-0" align="center" justify="center">
                <v-progress-circular
                  indeterminate
                  color="grey-lighten-5"
                ></v-progress-circular>
              </v-row>
            </template>
          </v-img>
          <v-card-item>
            <v-card-title>{{ blogPost.title }}</v-card-title>
            <v-card-subtitle>{{ blogPost.shortDescription }}</v-card-subtitle>
          </v-card-item>
          <v-card-actions>
            <router-link
              :to="{
                name: 'blogPost',
                params: { url: blogPost.urlHandle },
              }"
            >
              <v-btn prepend-icon="$info" variant="tonal" color="success">
                Read More
              </v-btn>
            </router-link>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
// Components
import AppHeader from "@/components/AppHeader.vue";

import { ref, onMounted } from "vue";

import axios from "axios";

const blogPosts = ref([]);

const getAllBlogPosts = () => {
  axios
    .get("https://localhost:7200/api/BlogPosts")
    .then((response) => {
      console.log(response);
      blogPosts.value = response.data;
    })
    .catch((err) => {
      console.log(err);
    });
};
// getAllBlogPosts();
onMounted(async () => {
  getAllBlogPosts();
});
</script>
