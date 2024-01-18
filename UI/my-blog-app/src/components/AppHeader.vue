<template>
  <div>
    <v-app-bar class="navigation_bar" scroll-threshold="1000" color="#558b2f">
      <template v-slot:prepend>
        <v-app-bar-nav-icon
          @click.stop="drawer = !drawer"
          class="nav-icon"
        ></v-app-bar-nav-icon>
      </template>

      <v-app-bar-title>
        <router-link :to="{ name: 'home' }"> BLOG APP </router-link>
      </v-app-bar-title>

      <v-text-field
        v-model="search"
        prepend-inner-icon="mdi-magnify"
        density="compact"
        label="Search"
        single-line
        flat
        hide-details
        variant="solo-filled"
      ></v-text-field>

      <v-spacer></v-spacer>

      <v-btn icon class="nav-icon">
        <v-icon>mdi-magnify</v-icon>
      </v-btn>

      <v-btn icon class="nav-icon">
        <v-icon>mdi-heart</v-icon>
      </v-btn>

      <v-btn>About</v-btn>
      <v-btn> Contact </v-btn>
      <v-btn>
        <v-icon>mdi-receipt</v-icon>
      </v-btn>
      <template>
        <v-btn>
          <v-icon>mdi-account</v-icon>
        </v-btn>
      </template>

      <v-avatar style="cursor: pointer" color="primary" variant="tonal">
        <v-img
          src="https://hoanghamobile.com/tin-tuc/wp-content/uploads/2023/07/logo-social-media.jpg"
        ></v-img>
        <!-- SECTION Menu -->
        <VMenu
          activator="parent"
          width="230"
          location="bottom end"
          offset="14px"
        >
          <VList>
            <!-- 👉 User Avatar & Name -->
            <VListItem>
              <template #prepend>
                <VListItemAction start>
                  <VBadge v-bind="avatarBadgeProps">
                    <VAvatar color="primary" size="40" variant="tonal">
                      <VImg :src="avatar1" />
                    </VAvatar>
                  </VBadge>
                </VListItemAction>
              </template>

              <VListItemTitle class="font-weight-semibold">
                John Doe
              </VListItemTitle>
              <VListItemSubtitle class="text-disabled">
                Admin
              </VListItemSubtitle>
            </VListItem>

            <VDivider class="my-2" />

            <!-- 👉 Profile -->
            <VListItem link>
              <template #prepend>
                <VIcon class="me-2" icon="mdi-account-outline" size="22" />
              </template>

              <VListItemTitle>Profile</VListItemTitle>
            </VListItem>

            <!-- 👉 Settings -->
            <VListItem link>
              <template #prepend>
                <VIcon class="me-2" icon="mdi-cog-outline" size="22" />
              </template>

              <VListItemTitle>Settings</VListItemTitle>
            </VListItem>

            <!-- 👉 Pricing -->
            <VListItem link>
              <template #prepend>
                <VIcon class="me-2" icon="mdi-currency-usd" size="22" />
              </template>

              <VListItemTitle>Pricing</VListItemTitle>
            </VListItem>

            <!-- 👉 FAQ -->
            <VListItem link>
              <template #prepend>
                <VIcon class="me-2" icon="mdi-help-circle-outline" size="22" />
              </template>

              <VListItemTitle>FAQ</VListItemTitle>
            </VListItem>

            <!-- Divider -->
            <VDivider class="my-2" />

            <!-- 👉 Logout -->
            <VListItem to="/login">
              <template #prepend>
                <VIcon class="me-2" icon="mdi-logout-variant" size="22" />
              </template>

              <VListItemTitle>Logout</VListItemTitle>
            </VListItem>
          </VList>
        </VMenu>
        <!-- !SECTION -->
      </v-avatar>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" temporary>
      <v-list density="compact" nav class="drawer_nav">
        <router-link :to="{ name: 'home' }">
          <v-list-item
            prepend-icon="mdi-view-dashboard"
            title="Home"
            value="home"
          ></v-list-item>
        </router-link>

        <v-list-group value="Admin">
          <template v-slot:activator="{ props }">
            <v-list-item
              v-bind="props"
              title="Admin"
              prepend-icon="mdi-account"
            ></v-list-item>
          </template>

          <router-link :to="{ name: 'admin-categories' }">
            <v-list-item
              prepend-icon="mdi-view-grid"
              title="Categories"
              value="categories"
            ></v-list-item>
          </router-link>

          <router-link :to="{ name: 'admin-blogposts' }">
            <v-list-item
              prepend-icon="mdi-content-paste"
              title="Blogs"
              value="blogs"
            >
            </v-list-item>
          </router-link>
        </v-list-group>
      </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script setup>
import { ref } from "vue";

const drawer = ref(null);
const search = ref(null);
</script>

<style lang="css" scoped>
.navigation_bar {
  color: #fff;
}

.navigation_bar a {
  text-decoration: none;
  color: #fff;
}

.drawer_nav a {
  text-decoration: none;
  color: #000;
}
.drawer_nav a:hover {
  text-decoration: none;
  color: #558b2f;
}
</style>
