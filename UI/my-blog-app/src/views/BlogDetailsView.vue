<template>
  <app-header />

  <v-container v-if="blogPost != {}">
    <div class="mx-auto">
      <v-img cover :src="blogPost.featureImageUrl"></v-img>

      <h1 class="mt-4">{{ blogPost.title }}</h1>
      <div class="d-flex justify-space-between mt-4">
        <span>{{ blogPost.author }}</span>
        <span>
          {{ date.format(blogPost.publishedDate, "fullDateWithWeekday") }}
        </span>
      </div>
      <div v-if="blogPost.categories" class="mt-4">
        <v-chip
          variant="flat"
          color="green"
          class="me-2"
          v-for="category in blogPost.categories"
          :key="category.id"
        >
          {{ category.name }}
        </v-chip>
      </div>
      <div class="mt-4 blog-content">
        <markdown>{{ blogPost.content }}</markdown>
      </div>
    </div>
  </v-container>
</template>

<script setup>
import AppHeader from "@/components/AppHeader.vue";

import { ref, onMounted } from "vue";
import { useRoute } from "vue-router";

import axios from "axios";
import { useDate } from "vuetify";

const blogPost = ref({});
const route = useRoute();

const date = useDate();

const getBlogPostByUrl = () => {
  axios
    .get(`https://localhost:7200/api/BlogPosts/${route.params.url}`)
    .then((response) => {
      // console.log(response);
      blogPost.value = response.data;
      console.log(blogPost.value);
    })
    .catch((err) => {
      console.log(err);
      // route.push({ name: "admin-blogposts" });
    });
};

onMounted(async () => {
  getBlogPostByUrl();
});
</script>

<style lang="scss" scoped></style>
