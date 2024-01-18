<template>
  <app-header />

  <v-container>
    <v-card>
      <v-form ref="form" @submit.prevent="onSubmit()">
        <v-card-title>
          <span class="text-h5">Add New Blog Post</span>
        </v-card-title>

        <v-card-text>
          <v-container>
            <v-row>
              <!-- <v-col cols="12">
                <v-text-field
                  v-if="editedIndex !== -1"
                  v-model="editedItem.id"
                  variant="solo"
                  label="ID"
                  disabled
                ></v-text-field>
              </v-col> -->
              <v-col cols="12">
                <v-text-field
                  v-model="editedItem.title"
                  variant="solo"
                  label="Title"
                  :rules="titleRules"
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  v-model="editedItem.shortDescription"
                  variant="solo"
                  label="Short Description"
                  :rules="shortDescriptionRules"
                ></v-text-field>
              </v-col>
              <!-- Content -->
              <v-col cols="12">
                <v-text-field
                  v-model="editedItem.content"
                  variant="solo"
                  label="Content"
                  :rules="contentRules"
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <CKEditorCustom />
              </v-col>
              <!-- Content -->
              <v-col
                cols="6"
                v-if="
                  editedItem.featureImageUrl !== null &&
                  editedItem.featureImageUrl !== ''
                "
              >
                <v-img
                  max-width="300"
                  cover
                  :src="editedItem.featureImageUrl"
                ></v-img>
              </v-col>
              <!-- Ảnh -->
              <v-col cols="12">
                <v-row>
                  <v-col cols="2">
                    <v-dialog v-model="dialog" max-width="500px">
                      <template v-slot:activator="{ props }">
                        <v-btn
                          color="primary"
                          dark
                          class="my-auto"
                          v-bind="props"
                          variant="text"
                        >
                          Upload Image
                        </v-btn>
                      </template>
                      <v-form ref="formUpload" @submit.prevent="onSubmitUpload">
                        <v-card>
                          <v-card-title>
                            <span class="text-h5">Upload Image</span>
                          </v-card-title>

                          <v-card-text>
                            <v-container>
                              <v-row>
                                <!-- Ảnh -->
                                <v-col cols="12">
                                  <v-file-input
                                    accept="image/png, image/jpeg, image/jpg"
                                    placeholder="Pick an image"
                                    label="Image"
                                    variant="solo"
                                    v-model="selectedFile"
                                    :rules="fileRules"
                                  >
                                    <template v-slot:append>
                                      <v-btn
                                        icon
                                        variant="text"
                                        @click="onUpload()"
                                      >
                                        <v-icon>mdi-upload</v-icon>
                                      </v-btn>
                                    </template>
                                  </v-file-input>
                                </v-col>
                              </v-row>
                            </v-container>
                          </v-card-text>
                          <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn
                              color="blue-darken-1"
                              variant="text"
                              @click="closeDialog"
                            >
                              Close
                            </v-btn>
                            <v-btn
                              color="blue-darken-1"
                              variant="text"
                              type="submit"
                            >
                              Upload
                            </v-btn>
                          </v-card-actions>
                        </v-card>
                      </v-form>
                    </v-dialog>
                  </v-col>
                  <v-col cols="10">
                    <v-text-field
                      v-model="editedItem.featureImageUrl"
                      variant="solo"
                      label="Feature Image Url"
                      :rules="featureImageUrlRules"
                    ></v-text-field>
                  </v-col>
                </v-row>
              </v-col>
              <!-- Ảnh -->
              <v-col cols="12">
                <v-text-field
                  v-model="editedItem.urlHandle"
                  variant="solo"
                  label="Url Handle"
                  :rules="urlHandleRules"
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  type="date"
                  v-model="editedItem.publishedDate"
                  variant="solo"
                  label="Published Date"
                  :rules="publishedDateRules"
                ></v-text-field>
              </v-col>

              <v-col cols="12">
                <v-text-field
                  v-model="editedItem.author"
                  variant="solo"
                  label="Author"
                  :rules="authorRules"
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-checkbox
                  label="Is invisible?"
                  v-model="editedItem.isVisible"
                ></v-checkbox>
              </v-col>
              <v-col cols="12">
                <v-select
                  label="Categories"
                  variant="solo"
                  multiple
                  hint="Pick blog post's categories"
                  persistent-hint
                  :items="categories"
                  item-title="name"
                  item-value="id"
                  v-model="editedItem.categories"
                  :rules="categoriesRules"
                ></v-select>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <router-link :to="{ name: 'admin-blogposts' }">
            <v-btn color="blue-darken-1" variant="text"> Cancel </v-btn>
          </router-link>
          <v-btn color="blue-darken-1" variant="text" type="submit">
            Save
          </v-btn>
        </v-card-actions>
      </v-form>
    </v-card>
  </v-container>
</template>

<script setup>
import AppHeader from "@/components/AppHeader.vue";
import CKEditorCustom from "./CKEditorCustom.vue";

import { ref } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";

const form = ref(null);
const router = useRouter();

//Validattion
const titleRules = [
  (value) => {
    if (value) return true;
    return "You must enter title";
  },
];
const shortDescriptionRules = [
  (value) => {
    if (value) return true;
    return "You must enter short description";
  },
];
const contentRules = [
  (value) => {
    if (value) return true;
    return "You must enter content";
  },
];
const featureImageUrlRules = [
  (value) => {
    if (value) return true;
    return "You must enter image";
  },
];

const urlHandleRules = [
  (value) => {
    if (value) return true;
    return "You must enter url handle";
  },
];

const publishedDateRules = [
  (value) => {
    if (value) return true;
    return "You must enter published date";
  },
];

const authorRules = [
  (value) => {
    if (value) return true;
    return "You must enter author";
  },
];

const categoriesRules = [
  (value) => {
    if (value) return true;
    return "You must enter category";
  },
];

const fileRules = [
  (value) => {
    if (value) return true;
    return "Please enter an image file";
  },
];

const dialog = ref(false);
// const editedIndex = ref(-1);
const editedItem = ref({});

const closeDialog = async () => {
  dialog.value = false;
  // await nextTick(() => {
  //   selectedFile.value = null;
  // });
};

const close = async () => {
  router.push({ name: "admin-blogposts" });
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

const createBlogPost = (item) => {
  axios
    .post("https://localhost:7200/api/BlogPosts", item)
    .then((response) => {
      console.log(response);
      // showSnackbar("Add blog post successfully!", "success");
      // getAllBlogPosts();
      router.push({ name: "admin-blogposts" });
    })
    .catch((err) => {
      // showSnackbar("Error", "red");
      console.log(err);
    });
};

const selectedFile = ref(null);
const uploadFile = (file) => {
  const formData = new FormData();
  formData.append("file", file.value[0]);
  formData.append("fileName", file.value[0].name.replace(/\.[^/.]+$/, ""));
  formData.append("title", file.value[0].name);
  axios
    .post("https://localhost:7200/api/Images", formData)
    .then((response) => {
      console.log(response);
      editedItem.value.featureImageUrl = response.data.url;
      // showSnackbar("Add image successfully!", "success");
      // getAllBlogPosts();
    })
    .catch((err) => {
      // showSnackbar("Error", "red");
      console.log(err);
    });
};

const onUpload = () => {
  uploadFile(selectedFile);
  console.log("Tên file: ", selectedFile.value);
  closeDialog();
};

const formUpload = ref(null);
const onSubmitUpload = async () => {
  const { valid } = await formUpload.value.validate();
  if (valid) {
    onUpload();
  }
};

const onSave = () => {
  // if (editedIndex.value > -1 || editedIndex.value !== -1) {
  //   editBlogPost(editedItem.value.id, editedItem.value);
  //   console.log(editedIndex);
  // } else {
  createBlogPost(editedItem.value);
  // uploadFile(selectedFile);
  // console.log("Tên file: ", selectedFile.value);
  // }
  close();
};

const onSubmit = async () => {
  const { valid } = await form.value.validate();
  if (valid) {
    onSave();
  }
};
</script>

<style lang="css" scoped></style>
