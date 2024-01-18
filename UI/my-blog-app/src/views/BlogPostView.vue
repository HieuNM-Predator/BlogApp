<template>
  <app-header />
  <v-container>
    <!-- Snack bar -->
    <v-snackbar
      v-model="snackbar"
      :timeout="2000"
      :color="color"
      rounded="pill"
      variant="elevated"
      position="relative"
      top="10"
    >
      {{ message }}
      <template v-slot:actions>
        <v-btn variant="text" @click="snackbar = false">
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </template>
    </v-snackbar>
    <!-- Table -->
    <v-data-table
      :headers="headers"
      :items="blogPosts"
      :height="500"
      fixed-header
    >
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>Blog Posts</v-toolbar-title>
          <v-spacer></v-spacer>
          <router-link :to="{ name: 'admin-addBlogPost' }"
            ><v-btn color="primary" dark class="mb-2"> New Blog Post </v-btn>
          </router-link>
          <v-dialog v-model="dialogDelete">
            <v-card class="mx-auto">
              <v-card-title class="text-h5 text-truncate"
                >Are you sure you want to delete this blogPost?</v-card-title
              >
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue-darken-1" variant="text" @click="closeDelete"
                  >Cancel</v-btn
                >
                <v-btn
                  color="blue-darken-1"
                  variant="text"
                  @click="deleteItemConfirm"
                  >OK</v-btn
                >
                <v-spacer></v-spacer>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-toolbar>
      </template>

      <template v-slot:item.isVisible="{ value }">
        <v-chip :color="getColor(value)"> {{ value }} </v-chip>
      </template>

      <template v-slot:item.categories="{ value }">
        <v-chip color="primary" v-for="(category, index) in value" :key="index">
          {{ category }}
        </v-chip>
      </template>

      <template v-slot:[`item.actions`]="{ item }">
        <router-link
          :to="{
            name: 'admin-editBlogPost',
            params: { blogPostId: item.id },
          }"
        >
          <v-icon color="success" size="small" class="me-2">
            mdi-pencil
          </v-icon>
        </router-link>
        <v-icon color="red" size="small" @click="deleteItem(item)">
          mdi-delete
        </v-icon>
      </template>
      <!-- <template v-slot:no-data>
        <v-btn color="primary" @click="initialize"> Reset </v-btn>
      </template> -->
    </v-data-table>
  </v-container>
</template>

<script setup>
import AppHeader from "@/components/AppHeader.vue";

import { ref, nextTick, watch, onMounted } from "vue";
import axios from "axios";

const headers = ref([
  {
    title: "Title",
    key: "title",
    sortable: false,
  },
  {
    title: "Short Description",
    key: "shortDescription",
    sortable: false,
  },
  {
    title: "Feature Image Url",
    key: "featureImageUrl",
    sortable: false,
  },
  {
    title: "Url Handle",
    key: "urlHandle",
    sortable: false,
  },
  {
    title: "Published Date",
    key: "publishedDate",
  },
  {
    title: "Author",
    key: "author",
    sortable: false,
  },
  {
    title: "Is Visible?",
    key: "isVisible",
    sortable: false,
  },
  {
    title: "Categories",
    key: "categories",
    value: (item) => {
      const categoryNames = item.categories.map((category) => category.name);
      return categoryNames;
    },
  },
  {
    title: "Actions",
    key: "actions",
    sortable: false,
  },
]);
const blogPosts = ref([]);

const editedIndex = ref(-1);
const editedItem = ref({});

//Đóng mở dialog
const dialog = ref(false);
const dialogDelete = ref(false);

const close = async () => {
  dialog.value = false;
  await nextTick(() => {
    editedIndex.value = -1;
    editedItem.value = {};
  });
};

const closeDelete = async () => {
  dialogDelete.value = false;
  await nextTick(() => {
    editedIndex.value = -1;
    editedItem.value = {};
  });
};

watch(dialog, (value) => {
  value || close();
});

watch(dialogDelete, (value) => {
  value || closeDelete();
});

watch(blogPosts.value, (value) => {
  value || getAllBlogPosts();
});

const getColor = (check) => {
  if (check === true) {
    return "green";
  } else {
    return "red";
  }
};

//Đóng mở snackbar
const snackbar = ref(false);
const message = ref("");
const color = ref("");
const showSnackbar = (msg, colour) => {
  message.value = msg;
  color.value = colour;
  snackbar.value = true;
};

//Get all categories to pick
const categories = ref([]);
const getAllCategories = () => {
  axios
    .get("https://localhost:7200/api/Categories")
    .then((response) => {
      categories.value = response.data.map((category) => {
        return {
          id: category.id,
          name: category.name,
          urlHandle: category.urlHandle,
        };
      });
      console.log(categories);
    })
    .catch((err) => {
      console.log(err);
    });
};
getAllCategories();

const deleteItem = (item) => {
  console.log(item);
  editedIndex.value = item.id;
  editedItem.value = Object.assign({}, item);
  dialogDelete.value = true;
};

// Call API
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

const deleteBlogPost = (blogPostId) => {
  axios
    .delete(`https://localhost:7200/api/BlogPosts/${blogPostId}`)
    .then((response) => {
      console.log(response);
      showSnackbar("Delete blog post successfully", "success");
      getAllBlogPosts();
    })
    .catch((err) => {
      showSnackbar("Error", "red");
      console.log(" Delete ERROR: ", err);
    });
};

const deleteItemConfirm = () => {
  deleteBlogPost(editedItem.value.id);
  closeDelete();
};
</script>

<style lang="css" scoped></style>
